# Inventory Management System

A production-oriented inventory and order management platform built on **.NET 10** with **Clean Architecture**, **CQRS**, exposed through a versioned REST API and consumed by a Windows desktop client.

The system tracks stock across multiple warehouses, manages the full order lifecycle (purchase, sale, transfer, returns), issues invoices as PDFs, and enforces role-based access with JWT authentication — backed by ~1,300 automated tests including subcutaneous tests against a real SQL Server test container.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927)
![Redis](https://img.shields.io/badge/Redis-Cache-DC382D)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED)
![Tests](https://img.shields.io/badge/tests-1300%2B-success)

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Domain Model](#domain-model)
- [Cross-Cutting Concerns](#cross-cutting-concerns)
- [Testing Strategy](#testing-strategy)
- [Getting Started](#getting-started)
- [API Reference](#api-reference)
- [Observability](#observability)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)

---

## Overview

This project manages the operational side of a distribution business: what stock exists, where it sits, how it moves, and who is allowed to move it.

It is built as a **layered monolith** with strict dependency rules. Business rules live in a pure domain layer with no framework dependencies. Use cases are modelled as explicit commands and queries. Infrastructure concerns — persistence, caching, PDF generation, email — sit behind interfaces the inner layers own.

**By the numbers:**

| Metric | Value |
|---|---|
| Projects in solution | 11 |
| REST endpoints | 112 across 20 controllers |
| CQRS handlers | 113 |
| Request validators | 66 |
| Domain aggregates & entities | 25+ |
| EF Core migrations | 21 |
| Automated tests | ~1,342 |
| Lines of C# (excl. designer files) | ~80,000 |

---

## Key Features

### Inventory & Stock Control
- Multi-warehouse stock tracking with per-warehouse, per-product quantities
- **Minimum stock levels** with low-stock detection and dashboard alerts
- **Reserved-quantity logic** — available stock accounts for quantities already committed to pending outbound orders and draft decrease-adjustments, preventing overselling
- Stock adjustments (increase/decrease) with a draft → applied workflow
- Product catalogue with categories, units of measure, and multi-image galleries

### Order Lifecycle
- Five order types: **Purchase, Sale, Transfer, Return-In, Return-Out**
- Enforced state machine (`Pending → Completed | Cancelled`) with locking once terminal
- Order-type-aware validation — a purchase requires a supplier, a sale requires a customer, a transfer requires a destination warehouse
- Line-item management with discounts, due dates, and computed subtotals
- Domain event (`OrderCompletedEvent`) triggers stock movement and downstream notification
- **Automatic cancellation** of overdue pending orders via a background service

### Invoicing
- Invoices issued against completed orders only, guarded at the domain level
- **PDF generation via QuestPDF**, with embedded fonts for Arabic and emoji rendering
- One-invoice-per-order invariant enforced in the aggregate

### Identity & Authorization
- JWT bearer authentication with **refresh token rotation** and background revocation of expired tokens
- Five roles: `Admin`, `SalesUser`, `PurchasesUser`, `WarehouseUser`, `Viewer`
- Per-endpoint role authorization plus a **custom authorization policy** (`WarehouseUpdateRequirement`) for resource-scoped permissions
- Password hashing, user management, and admin-driven password reset

### Parties & Reference Data
- People, Employees, Customers, Suppliers, and supplier-product catalogues
- Identity documents with image upload and expiry tracking
- Normalized address hierarchy (Country → City → Address) and contact information

### Auditing & Compliance
- Automatic `CreatedBy` / `CreatedAt` / `LastModifiedBy` / `LastModifiedAt` stamping via EF Core interceptors
- **Soft delete** via interceptor + query filters — records are flagged, never destroyed
- Dedicated audit logs for login events and user operations
- **Working-hours middleware** restricting write operations to configured business hours
- Per-user timezone resolution applied at the serialization boundary

---

## Architecture

Clean Architecture with dependencies pointing strictly inward:

```
┌──────────────────────────────────────────────────────────┐
│  Presentation                                            │
│  ├── InventoryManagementSystemAPI  (ASP.NET Core, .NET 10)│
│  └── UI                            (WinForms, .NET 4.8)   │
├──────────────────────────────────────────────────────────┤
│  Infrastructure                                          │
│  EF Core · Redis · JWT · QuestPDF · MimeKit · Workers     │
├──────────────────────────────────────────────────────────┤
│  Application                                             │
│  CQRS handlers · Behaviors · Validators · Mappers · DTOs  │
├──────────────────────────────────────────────────────────┤
│  Domain                                                  │
│  Aggregates · Value objects · Domain events · Errors      │
│  (zero infrastructure dependencies)                       │
└──────────────────────────────────────────────────────────┘
```

### Design decisions worth calling out

**Result pattern instead of exceptions for business failures.**
A custom `Result<T>` type carries either a value or a list of typed `Error` records (`Validation`, `NotFound`, `Conflict`, `Unauthorized`, `Forbidden`, `Failure`). Implicit conversions keep handler code clean:

```csharp
public static Result<WarehouseStock> Create(Guid id, Guid warehouseId, Guid productId,
                                            decimal minimumStockLevel, decimal quantity = 0)
{
    if (warehouseId == Guid.Empty)      return WarehouseStockErrors.WarehouseRequired;
    if (productId   == Guid.Empty)      return WarehouseStockErrors.ProductRequired;
    if (minimumStockLevel < 0)          return WarehouseStockErrors.MinimumStockLevelInvalid;

    return new WarehouseStock(id, warehouseId, productId, quantity, minimumStockLevel);
}
```

Controllers translate results to HTTP with a single `Match` call, and the shared `ApiController` base maps each `ErrorKind` onto the correct status code and an **RFC 7807 ProblemDetails** payload. Exceptions are reserved for genuinely exceptional conditions.

**Encapsulated aggregates.**
Entities expose private setters, private constructors, and static factory methods. Collections are exposed as `IReadOnlyCollection<T>` over private backing lists. Invalid state is unrepresentable — `Order.UpdateStatus` refuses illegal transitions rather than trusting the caller.

**Feature-sliced application layer.**
Each use case is a folder containing its command/query, handler, validator, and DTOs, grouped by bounded context (`Inventory`, `Transactions`, `Parties`, `References`, `Identity`, `Users`, `Dashboard`).

**Contract duplication for client compatibility.**
`Contracts` targets .NET 10 for the API; `ContractOldCompatibile` mirrors it for the .NET Framework 4.8 WinForms client, keeping request/response shapes in sync across two runtimes.

### MediatR pipeline

Every request flows through a composed pipeline before reaching its handler:

```
Request
  └─ LoggingPreProcessor
      └─ UnhandledExceptionBehaviour   ← safety net, structured logging
          └─ PerformanceBehaviour      ← flags slow requests
              └─ ValidationBehavior    ← FluentValidation → Result errors
                  └─ CachingBehavior   ← ICachedQuery opt-in, HybridCache
                      └─ Handler
              └─ LoggingPostProcessor
```

---

## Tech Stack

| Concern | Technology |
|---|---|
| Runtime | .NET 10 |
| API | ASP.NET Core, API Versioning (Asp.Versioning) |
| Mediation | MediatR 14 |
| Validation | FluentValidation 12 |
| Persistence | EF Core 10, SQL Server 2022 |
| Caching | Redis + `HybridCache` (L1 in-memory / L2 distributed), Output Cache |
| Auth | JWT Bearer, refresh tokens, policy-based authorization |
| Logging | Serilog → Console + Seq |
| Telemetry | OpenTelemetry → OTLP traces, Prometheus metrics, Grafana |
| PDF | QuestPDF |
| Email | MimeKit |
| Docs | OpenAPI, Scalar, Swagger UI |
| Resilience | Polly |
| Testing | xUnit, FluentAssertions, Testcontainers, WebApplicationFactory |
| Desktop client | WinForms (.NET Framework 4.8) |
| Orchestration | Docker Compose |

---

## Domain Model

```
Order ──┬── OrderDetail (line items)
        ├── Invoice ── InvoiceLineItem
        ├── Supplier / Customer
        └── SourceWarehouse / DestinationWarehouse

Warehouse ── WarehouseStock ── Product ──┬── Category
                                          ├── ProductImage
                                          └── SupplierProduct ── Supplier

Adjustment ── AdjustmentDetail ── Product

Person ──┬── Employee ── User ── RefreshToken
         ├── Customer
         ├── Document
         ├── Address ── City ── Country
         └── ContactInfo

AuditLog ──┬── UserLoginAuditLog
           └── UserOperationsAuditLog
```

Persistence is configured entirely through **25 `IEntityTypeConfiguration` classes** — no data annotations leak into the domain. `WarehouseStock` carries a `RowVersion` column for **optimistic concurrency**, so simultaneous stock movements fail loudly instead of silently losing updates.

---

## Cross-Cutting Concerns

| Concern | Implementation |
|---|---|
| **Caching** | Two-tier `HybridCache` (local + Redis) via opt-in `ICachedQuery`; tag-based Output Cache invalidation per entity; ETag support for conditional requests; Redis health probed continuously with graceful degradation |
| **Rate limiting** | Per-IP sliding-window global limiter (100 req/min) plus a stricter fixed-window limiter on auth endpoints (5 req/min) to blunt credential stuffing |
| **Error handling** | Global exception handler + `ProblemDetails` enriched with request ID and instance path |
| **Compression** | Brotli and Gzip response compression, HTTPS-enabled |
| **Background work** | Overdue order cancellation · refresh token revocation · Redis health monitoring |
| **API versioning** | URL-segment versioning (`/api/v{version}/…`) with per-version OpenAPI documents |
| **Time handling** | UTC storage with per-user timezone conversion applied by custom JSON converters resolved from the request context |

---

## Testing Strategy

Roughly **1,342 test methods** across four projects, organized as a test pyramid:

| Project | Tests | Scope |
|---|---:|---|
| `DomainTesting` | 286 | Aggregate invariants, state transitions, factory validation — pure, fast, no I/O |
| `ApplicationTesting` | 62 | Mapper correctness and pipeline behavior wiring |
| `SubcutaneousTests` | 994 | Full-stack integration through the real HTTP surface |
| `CommonTesting` | — | Shared entity factories and fixtures consumed by the suites above |

**Integration tests run against a real SQL Server** spun up per-run with **Testcontainers**, driven through `WebApplicationFactory`. They exercise the genuine pipeline — routing, auth, validation, EF Core, interceptors — rather than mocking it away. Shared test data factories keep arrangement terse and consistent.

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Visual Studio 2022+ or JetBrains Rider (required only for the WinForms client)

### Run with Docker Compose

```bash
git clone https://github.com/<your-username>/inventory-management-system.git
cd inventory-management-system

# Provide secrets (see Configuration below) then:
docker compose up -d
```

This starts the API alongside SQL Server, Redis, Seq, Prometheus, and Grafana. The database is created and seeded automatically on first run in the Development environment.

| Service | URL |
|---|---|
| API | http://localhost:5001 |
| Scalar API reference | http://localhost:5001/scalar/v1 |
| Swagger UI | http://localhost:5001/swagger |
| Health check | http://localhost:5001/health |
| Prometheus metrics | http://localhost:5001/metrics |
| Seq (logs & traces) | http://localhost:5111 |
| Prometheus | http://localhost:9090 |
| Grafana | http://localhost:5005 |

### Run locally

```bash
dotnet restore
dotnet ef database update --project Infrastructure --startup-project InventoryManagementSystemAPI
dotnet run --project InventoryManagementSystemAPI
```

### Run the tests

```bash
dotnet test
```

> Integration tests require a running Docker daemon — Testcontainers provisions SQL Server automatically.

### Desktop client

Open the solution in Visual Studio, set `UI` as the startup project, point `App.config` at your API base URL, and run. The client stores tokens in protected local storage and refreshes them transparently.

### Configuration

Configure via `appsettings.json`, environment variables, or user secrets:

| Key | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `JwtSettings:Secret` | Signing key — **must** be supplied as a secret |
| `JwtSettings:TokenExpirationInMinutes` | Access token lifetime (default 15) |
| `JwtSettings:RefreshTokenExpirationInDays` | Refresh token lifetime (default 7) |
| `Caching:Redis:ConnectionString` | Redis endpoint |
| `RateLimiting:*` | Global and auth-endpoint limits |
| `AppSettings:OpenAt` / `CloseAt` | Business hours enforced by middleware |
| `Otlp:Endpoint` | OpenTelemetry collector endpoint |

> **Never commit real secrets.** Use `dotnet user-secrets` for local development and environment variables or a managed vault in deployed environments.

---

## API Reference

All routes are versioned under `/api/v1/`. Every endpoint except authentication requires a bearer token.

| Resource | Endpoints | Capabilities |
|---|---:|---|
| `identity` | 2 | Login, refresh token |
| `orders` | 11 | CRUD, line items, status transitions, paged queries |
| `adjustments` | 11 | CRUD, detail lines, status workflow |
| `suppliers` | 10 | CRUD, supplier-product catalogue |
| `people` | 8 | CRUD, images, documents |
| `products` | 8 | CRUD, image gallery, paged search |
| `users` | 7 | CRUD, role assignment, password reset |
| `countries` · `documents` | 6 each | Reference data management |
| `warehouses` · `warehouse-stocks` · `customers` · `employees` · `categories` · `addresses` · `contact-infos` | 5 each | CRUD + paged queries |
| `cities` | 4 | Reference data |
| `invoices` | 3 | Issue, retrieve, download PDF |
| `dashboard` | 1 | Aggregated KPIs and low-stock alerts |

Interactive documentation is served by **Scalar** and **Swagger UI**, including bearer-token security definitions injected by a custom OpenAPI transformer.

**Example — creating a sale order**

```http
POST /api/v1/orders
Authorization: Bearer {token}
Content-Type: application/json

{
  "orderType": "Sale",
  "customerId": "3f2b...",
  "sourceWarehouseId": "9a1c...",
  "dueDate": "2026-09-15T00:00:00Z",
  "discountAmount": 25.00,
  "notes": "Priority delivery",
  "orderDetails": [
    { "productId": "7d4e...", "quantity": 10, "unitPrice": 49.99 }
  ]
}
```

The handler validates the request, checks **available** stock (on-hand minus reserved), constructs the aggregate through its factory, and returns either the created order or a `ProblemDetails` describing exactly which invariant failed.

---

## Observability

- **Structured logging** — Serilog with request logging, enrichment, and a Seq sink
- **Distributed tracing** — OpenTelemetry instrumentation for ASP.NET Core and `HttpClient`, exported over OTLP
- **Metrics** — Prometheus scraping endpoint, visualized in Grafana
- **Health checks** — `/health` endpoint covering SQL Server connectivity
- **Performance monitoring** — pipeline behavior that logs slow requests with their originating user

---

## Project Structure

```
├── Domain/                     Aggregates, domain events, errors, Result<T>
├── Application/                CQRS handlers, behaviors, validators, mappers
├── Infrastructure/             EF Core, Redis, JWT, PDF, email, workers
├── InventoryManagementSystemAPI/   Controllers, middleware, OpenAPI, DI
├── Contracts/                  Shared request/response contracts (.NET 10)
├── ContracOldCompatibile/      Mirrored contracts (.NET Framework 4.8)
├── UI/                         WinForms desktop client
├── DomainTesting/              Domain unit tests
├── ApplicationTesting/         Application-layer unit tests
├── SubcutaneousTests/          End-to-end integration tests (Testcontainers)
├── CommonTesting/              Shared test factories
└── docker-compose.yml          API + SQL Server + Redis + Seq + Prometheus + Grafana
```

---

## Roadmap

- [ ] CI/CD pipeline with automated test runs and coverage reporting
- [ ] Web front-end (Blazor or React) to complement the desktop client
- [ ] Barcode / QR scanning for stock operations
- [ ] Purchase-order approval workflows
- [ ] Advanced reporting and data export
- [ ] Multi-tenancy support

---

## License

This project is licensed under the MIT License.

## Contact

Built by **[Salah Mohammed Yaghi]** — [https://www.linkedin.com/in/salah-yaghi-3935b3364/](#) · [yaghimohsalah@gmail.com](#)
