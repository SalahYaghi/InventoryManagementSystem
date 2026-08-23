using Domain.Adjustments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AdjustmentConfiguration : IEntityTypeConfiguration<Adjustment>
    {
        public void Configure(EntityTypeBuilder<Adjustment> builder)
        {
            builder.ToTable("Adjustments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WarehouseId).IsRequired();
            builder.Property(x => x.AdjustmentType).HasConversion<int>().IsRequired();
            builder.Property(x => x.AdjustmentReason).HasConversion<int>().IsRequired();
            builder.Property(x => x.AdjustmentStatus).HasConversion<int>().IsRequired();
            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.Property(x => x.AprovedAt).IsRequired(false);

            builder.HasOne(x => x.Warehouse)
                   .WithMany()
                   .HasForeignKey(x => x.WarehouseId)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        }
    }
}

