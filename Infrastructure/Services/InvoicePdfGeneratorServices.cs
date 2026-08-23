using Contract.Common.Interfaces;
using Domain.Contacts.Address;
using Domain.Invoices;
using Domain.Warehouses;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public sealed class InvoicePdfGeneratorServices : IInvoicePdfGenerator
    {
        private const string AccentColor = "#1E3A8A";
        private const string LightAccent = "#EFF6FF";
        private const string BorderColor = "#E5E7EB";
        private const string DarkText = "#111827";
        private const string MutedText = "#6B7280";

        public byte[] Generate(Invoice invoice)
        {
            ArgumentNullException.ThrowIfNull(invoice);

            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9).FontColor(DarkText));

                    page.Header().Element(c => ComposeHeader(c, invoice));
                    page.Content().Element(c => ComposeContent(c, invoice));
                    page.Footer().Element(ComposeFooter);
                });
            }).GeneratePdf();
        }

        private static void ComposeHeader(IContainer container, Invoice invoice)
        {
            container.Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(left =>
                    {
                        left.Item().Text("Inventory Management System")
                            .FontSize(18)
                            .Bold()
                            .FontColor(AccentColor);

                        left.Item().PaddingTop(4).Text("Professional invoice document")
                            .FontSize(9)
                            .FontColor(MutedText);

                        left.Item().PaddingTop(8).Text($"Generated At: {DateTimeOffset.Now:yyyy-MM-dd HH:mm}")
                            .FontSize(8)
                            .FontColor(MutedText);
                    });

                    row.ConstantItem(220).AlignRight().Column(right =>
                    {
                        right.Item().AlignRight().Text(GetInvoiceTitle(invoice))
                            .FontSize(20)
                            .Bold()
                            .FontColor(AccentColor);

                        right.Item().AlignRight().PaddingTop(5).Text($"Invoice ID: {ShortId(invoice.Id)}")
                            .FontSize(9);

                        right.Item().AlignRight().PaddingTop(2).Text($"Status: {invoice.Status}")
                            .FontSize(9)
                            .SemiBold();

                        right.Item().AlignRight().PaddingTop(2).Text($"Due Date: {FormatDate(invoice.Order?.DueDate)}")
                            .FontSize(9);
                    });
                });

                column.Item().PaddingTop(15).LineHorizontal(1).LineColor(BorderColor);
            });
        }

        private static void ComposeContent(IContainer container, Invoice invoice)
        {
            container.PaddingTop(15).Column(column =>
            {
                column.Spacing(14);

                column.Item().Element(c => ComposePartyAndWarehouse(c, invoice));
                column.Item().Element(c => ComposeOrderSummary(c, invoice));
                column.Item().Element(c => ComposeItemsTable(c, invoice));
                column.Item().Element(c => ComposeTotals(c, invoice));
                column.Item().Element(c => ComposeNotes(c, invoice));
                column.Item().Element(c => ComposeSignatures(c , invoice));
            });
        }

        private static void ComposePartyAndWarehouse(IContainer container, Invoice invoice)
        {
            container.Row(row =>
            {
                row.RelativeItem().Element(c => ComposePartyCard(c, invoice));
                row.ConstantItem(15);
                row.RelativeItem().Element(c => ComposeWarehouseCard(c, invoice.Order?.SourceWarehouse, "Source Warehouse"));
            });
        }

        private static void ComposePartyCard(IContainer container, Invoice invoice)
        {
            var isPurchaseOrReturnOut = invoice.InvoiceType == InvoiceType.Purchase || invoice.InvoiceType == InvoiceType.ReturnOut;

            if (isPurchaseOrReturnOut)
            {
                var supplier = invoice.Order?.Supplier;

                ComposeInfoCard(
                    container,
                    "Supplier Information",
                    new[]
                    {
                    ("Name", supplier?.SupplierName),
                    ("Code", supplier?.SupplierCode),
                    ("Phone", supplier?.Contact?.PhoneNumber),
                    ("Alternative Phone", supplier?.Contact?.AlternitavePhoneNumber),
                    ("Email", supplier?.Contact?.Email),
                    ("Fax", supplier?.Contact?.FaxNumber),
                    ("Website", supplier?.Contact?.WebsiteUrl),
                    ("Address", FormatAddress(supplier?.Address))
                    });
            }
            else
            {
                var customer = invoice.Order?.Customer;

                ComposeInfoCard(
                    container,
                    "Customer Information",
                    new[]
                    {
                    ("Name", customer?.CustomerName),
                    ("Code", customer?.CustomerCode),
                    ("Phone", customer?.Contact?.PhoneNumber),
                    ("Alternative Phone", customer?.Contact?.AlternitavePhoneNumber),
                    ("Email", customer?.Contact?.Email),
                    ("Fax", customer?.Contact?.FaxNumber),
                    ("Website", customer?.Contact?.WebsiteUrl),
                    ("Address", FormatAddress(customer?.Address))
                    });
            }
        }

        private static void ComposeWarehouseCard(IContainer container, Warehouse? warehouse, string title)
        {
            ComposeInfoCard(
                container,
                title,
                new[]
                {
                ("Name", warehouse?.Name),
                ("Code", warehouse?.Code),
                ("Status", warehouse?.WarehouseStatus.ToString()),
                ("Address", FormatAddress(warehouse?.Address))
                });
        }

        private static void ComposeOrderSummary(IContainer container, Invoice invoice)
        {
            var order = invoice.Order;

            ComposeInfoCard(
                container,
                "Order & Invoice Summary",
                new[]
                {
                ("Order ID", ShortId(order?.Id)),
                ("Invoice ID", ShortId(invoice.Id)),
                ("Invoice Type", invoice.InvoiceType.ToString()),
                ("Invoice Status", invoice.Status.ToString()),
                ("Order Type", order?.OrderType.ToString()),
                ("Order Status", order?.OrderStatus.ToString()),
                ("Created By", invoice.CreatedBy),
                ("Created At", FormatDate(invoice.CreatedAtUtc)),
                ("Last Modified By", invoice.LastModifiedBy),
                ("Last Modified At", FormatDate(invoice.LastModifiedUtc))
                });
        }

        private static void ComposeItemsTable(IContainer container, Invoice invoice)
        {
            container.Column(column =>
            {
                column.Item().Text("Invoice Items")
                    .FontSize(12)
                    .Bold()
                    .FontColor(AccentColor);

                column.Item().PaddingTop(6).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1.4f);
                        columns.RelativeColumn(1.4f);
                        columns.RelativeColumn(1.6f);
                    });

                    table.Header(header =>
                    {
                        HeaderCell(header.Cell(), "#");
                        HeaderCell(header.Cell(), "Description");
                        HeaderCell(header.Cell(), "Qty");
                        HeaderCell(header.Cell(), "Unit Price");
                        HeaderCell(header.Cell(), "Tax");
                        HeaderCell(header.Cell(), "Total");
                    });

                    foreach (var item in invoice.LineItems.OrderBy(x => x.LineNo))
                    {
                        BodyCell(table.Cell(), item.LineNo.ToString());
                        BodyCell(table.Cell(), item.Description);
                        BodyCell(table.Cell(), FormatNumber(item.Quantity));
                        BodyCell(table.Cell(), FormatMoney(item.UnitPrice));
                        BodyCell(table.Cell(), FormatMoney(item.Tax));
                        BodyCell(table.Cell(), FormatMoney(item.TotalAmount), true);
                    }
                });
            });
        }

        private static void ComposeTotals(IContainer container, Invoice invoice)
        {
            container.Row(row =>
            {
                row.RelativeItem();

                row.ConstantItem(260).Background(LightAccent).Border(1).BorderColor(BorderColor).Padding(12).Column(column =>
                {
                    column.Spacing(6);

                    TotalRow(column, "Subtotal", invoice.SubTotalAmount);
                    TotalRow(column, "Discount", invoice.DiscountAmount);
                    TotalRow(column, "Tax", invoice.TaxAmount);

                    column.Item().PaddingVertical(4).LineHorizontal(1).LineColor(BorderColor);

                    column.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Net Amount").FontSize(12).Bold().FontColor(AccentColor);
                        r.ConstantItem(110).AlignRight().Text(FormatMoney(invoice.NetAmount)).FontSize(12).Bold().FontColor(AccentColor);
                    });
                });
            });
        }

        private static void ComposeNotes(IContainer container, Invoice invoice)
        {
            if (string.IsNullOrWhiteSpace(invoice.Notes))
                return;

            container.Border(1).BorderColor(BorderColor).Padding(10).Column(column =>
            {
                column.Item().Text("Notes").FontSize(11).Bold().FontColor(AccentColor);
                column.Item().PaddingTop(5).Text(invoice.Notes).FontSize(9);
            });
        }

        private static void ComposeSignatures(IContainer container, Invoice invoice)
        {
            string receivedBy =
                invoice.Order.OrderType == Domain.Orders.OrderType.Sale ||
                invoice.Order.OrderType == Domain.Orders.OrderType.ReturnIn
                    ? invoice.Order.Customer!.CustomerName
                    : invoice.Order.Supplier!.SupplierName;

            string preparedBy = "Inventory Management System";
            string approvedBy = invoice.CreatedBy ?? "Undefined";

            container.PaddingTop(20).Row(row =>
            {
                SignatureBox(row.RelativeItem(), "Prepared By", preparedBy);
                row.ConstantItem(20);

                SignatureBox(row.RelativeItem(), "Received By", receivedBy);
                row.ConstantItem(20);

                SignatureBox(row.RelativeItem(), "Approved By", approvedBy);
            });
        }
        private static void ComposeFooter(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().LineHorizontal(1).LineColor(BorderColor);

                column.Item().PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Text("Generated by Inventory Management System")
                        .FontSize(8)
                        .FontColor(MutedText);

                    row.ConstantItem(120).AlignRight().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(8).FontColor(MutedText));
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
                });
            });
        }

        private static void ComposeInfoCard(IContainer container, string title, IEnumerable<(string Label, string? Value)> rows)
        {
            container.Border(1).BorderColor(BorderColor).Background(Colors.White).Padding(10).Column(column =>
            {
                column.Item().Text(title)
                    .FontSize(11)
                    .Bold()
                    .FontColor(AccentColor);

                column.Item().PaddingTop(8).Column(inner =>
                {
                    inner.Spacing(4);

                    foreach (var row in rows)
                    {
                        if (string.IsNullOrWhiteSpace(row.Value))
                            continue;

                        inner.Item().Row(r =>
                        {
                            r.ConstantItem(105).Text(row.Label)
                                .FontSize(8)
                                .SemiBold()
                                .FontColor(MutedText);

                            r.RelativeItem().Text(row.Value)
                                .FontSize(8)
                                .FontColor(DarkText);
                        });
                    }
                });
            });
        }

        private static void HeaderCell(IContainer container, string text)
        {
            container.Background(AccentColor)
                .PaddingVertical(6)
                .PaddingHorizontal(5)
                .Text(text)
                .FontColor(Colors.White)
                .Bold()
                .FontSize(8);
        }

        private static void BodyCell(IContainer container, string text, bool bold = false)
        {
            var descriptor = container
                .BorderBottom(1)
                .BorderColor(BorderColor)
                .PaddingVertical(6)
                .PaddingHorizontal(5)
                .Text(text)
                .FontSize(8);

            if (bold)
                descriptor.Bold();
        }

        private static void TotalRow(ColumnDescriptor column, string label, decimal value)
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Text(label).FontSize(9).FontColor(MutedText);
                row.ConstantItem(110).AlignRight().Text(FormatMoney(value)).FontSize(9).SemiBold();
            });
        }
        private static void SignatureBox(IContainer container, string title, string value)
        {
            container.Border(1)
                .BorderColor(BorderColor)
                .Padding(10)
                .Column(column =>
                {
                    column.Item()
                        .AlignCenter()
                        .Text(value)
                        .FontSize(9)
                        .SemiBold()
                        .FontColor(DarkText);

                    column.Item().Height(25);

                    column.Item()
                        .LineHorizontal(1)
                        .LineColor(BorderColor);

                    column.Item()
                        .PaddingTop(4)
                        .AlignCenter()
                        .Text(title)
                        .FontSize(8)
                        .FontColor(MutedText);
                });
        }
        private static string GetInvoiceTitle(Invoice invoice)
        {
            return invoice.InvoiceType switch
            {
                InvoiceType.Purchase => "PURCHASE INVOICE",
                InvoiceType.Sale => "SALES INVOICE",
                InvoiceType.ReturnIn => "Return In INVOICE",
                InvoiceType.ReturnOut => "Return Out INVOICE",
                _ => "INVOICE"
            };
        }

        private static string FormatAddress(Address? address)
        {
            if (address is null)
                return "-";

            var parts = new[]
            {
            address.BuildingNumber,
            address.Street,
            address.City?.Name,
            address.Country?.Name,
            address.PostalCode,
            address.Description
        };

            var result = string.Join(", ", parts.Where(x => !string.IsNullOrWhiteSpace(x)));

            return string.IsNullOrWhiteSpace(result) ? "-" : result;
        }

        private static string FormatDate(DateTimeOffset? date)
        {
            return date.HasValue && date.Value != default
                ? date.Value.ToLocalTime().ToString("yyyy-MM-dd")
                : "-";
        }

        private static string FormatMoney(decimal value)
        {
            return $"{value:N2}";
        }

        private static string FormatNumber(decimal value)
        {
            return value.ToString("N2");
        }

        private static string ShortId(Guid id)
        {
            return id == Guid.Empty ? "-" : id.ToString("N")[..8].ToUpper();
        }

        private static string ShortId(Guid? id)
        {
            return id.HasValue ? ShortId(id.Value) : "-";
        }
    }
}
