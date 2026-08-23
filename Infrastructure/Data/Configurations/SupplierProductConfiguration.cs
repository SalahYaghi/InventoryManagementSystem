using Domain.Suppliers.SupplierProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
    {
        public void Configure(EntityTypeBuilder<SupplierProduct> builder)
        {
            builder.ToTable("SupplierProducts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SupplierId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.PurchasePrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
            builder.Property(x => x.RowVersion).IsRowVersion();


            builder.HasOne(s => s.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.SupplierId, x.ProductId }).IsUnique();
        }
    }
}

