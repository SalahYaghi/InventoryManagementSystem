using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products" , t => t.HasTrigger("After_Product_Update_Trigger"));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SKU).HasMaxLength(10).IsRequired();
            builder.Property(x => x.BarCode).HasMaxLength(50);
            builder.Property(x => x.ProductName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.SellingPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Unit).HasConversion<int>().IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

       
  
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SKU).IsUnique();
        }
    }
}

