using Domain.Adjustments;
using Domain.AuditLoggs;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Contacts.ContactInfo;
using Domain.Customer;
using Domain.Document;
using Domain.Identity.Employee;
using Domain.Identity.RefreshToken;
using Domain.Identity.Users;
using Domain.Invoices;
using Domain.Orders;
using Domain.People;
using Domain.Products;
using Domain.Products.Category;
using Domain.Products.Domain.Products;
using Domain.Suppliers;
using Domain.Suppliers.SupplierProducts;
using Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.Common.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Employee> Employees { get; }
        public DbSet<UserLoginAuditLog> UserLoginAuditLoggs { get; }
        public DbSet<UserOperationsAuditLog> UserOperationsAuditLog { get; }

        public DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Adjustment> Adjustments { get; }
        DbSet<AdjustmentDetail> AdjustmentDetails { get; }
        DbSet<Address> Addresses { get; }
        DbSet<City> Cities { get; }
        DbSet<Country> Countries { get; }
        DbSet<ContactInfo> ContactInfos { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Document> Documents { get; }
        public DbSet<Invoice> Invoices { get; }
        DbSet<InvoiceLineItem> InvoiceLineItems { get; }

        DbSet<Order> Orders { get; }
        DbSet<OrderDetail> OrderDetails { get; }
        DbSet<Person> People { get; }
        DbSet<ProductImage> ProductImages { get; }
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<SupplierProduct> SupplierProducts { get; }
        DbSet<Warehouse> Warehouses { get; }
      
        DbSet<WarehouseStock> WarehouseStocks { get; }

        void ClearChangeTracker();

        string GetConnectionString();
        Task<bool> CanConnectAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

