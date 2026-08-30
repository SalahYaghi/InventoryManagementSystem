using Contract.Common.Interfaces;
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
using Inventory.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator sender)  : DbContext(options) , IAppDbContext
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<Adjustment> Adjustments => Set<Adjustment>();
        public DbSet<AdjustmentDetail> AdjustmentDetails => Set<AdjustmentDetail>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceLineItem> LineItems => Set<InvoiceLineItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<SupplierProduct> SupplierProducts => Set<SupplierProduct>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<WarehouseStock> WarehouseStocks => Set<WarehouseStock>();

        public DbSet<InvoiceLineItem> InvoiceLineItems => Set<InvoiceLineItem>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<UserLoginAuditLog> UserLoginAuditLoggs => Set<UserLoginAuditLog>();
        public DbSet<UserOperationsAuditLog> UserOperationsAuditLog => Set<UserOperationsAuditLog>();

        public Task<bool> CanConnectAsync(CancellationToken ct)
        => Database.CanConnectAsync(ct);

        public void ClearChangeTracker()
        {
            ChangeTracker.Clear();
        }

        public string GetConnectionString()
        {
            return Database.GetConnectionString() ?? string.Empty;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()){

                foreach (var property in entityType.GetProperties()) {

                    if (property.ClrType == typeof(string))
                    {
                        if (property.IsNullable)
                        {
                            property.SetValueConverter(new ValueConverter<string?, string?>(
                            v => string.IsNullOrEmpty(v) ? null : v,
                            v => v
                        ));
                        }
                    }

                }


            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            await DispatchDomainEventsAsync(cancellationToken); 
            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken) {

            var domainEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is Entity baseEntity && baseEntity.DomainEvents.Count > 0)
                .Select(e => ((Entity)e.Entity))
                .ToList();

            var events = domainEntities
                .SelectMany(e => e.DomainEvents)
                .ToList(); 
          
            foreach (var e in events) {

                await sender.Publish(e , cancellationToken); 

            }

            foreach (var entity in domainEntities) {

                 entity.ClearDomainEvents();
            }



        }

}
}


