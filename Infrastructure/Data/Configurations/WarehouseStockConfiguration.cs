using Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class WarehouseStockConfiguration : IEntityTypeConfiguration<WarehouseStock>
    {
        public void Configure(EntityTypeBuilder<WarehouseStock> builder)
        {
            builder.ToTable("WarehouseStocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WarehouseId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.MinimumStockLevel).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.HasOne(w => w.Warehouse)
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.WarehouseStock)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
             builder.HasQueryFilter(x => x.IsDeleted != true);
            builder.HasIndex(x => new { x.WarehouseId, x.ProductId })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");
        }
    }
}

