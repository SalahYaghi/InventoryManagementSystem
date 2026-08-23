using Domain.Invoices;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderType).HasConversion<int>().IsRequired();
            builder.Property(x => x.OrderStatus).HasConversion<int>().IsRequired();
            builder.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)").IsRequired(false);
            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

            builder.Property(x => x.DueDate)
                   .IsRequired(); 

            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(s => s.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Order>(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.SourceWarehouse)
                .WithMany()
                .HasForeignKey(x => x.SourceWarehouseId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(w => w.DestinationWarehouse)
                .WithMany()
                .HasForeignKey(x => x.DestinationWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}

