using Domain.Adjustments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AdjustmentDetailConfiguration : IEntityTypeConfiguration<AdjustmentDetail>
    {
        public void Configure(EntityTypeBuilder<AdjustmentDetail> builder)
        {
            builder.ToTable("AdjustmentDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AdjustmentId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

            builder.HasOne(x => x.Adjustment)
                .WithMany(x => x.AdjustmentDetails)
                .HasForeignKey(x => x.AdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RowVersion).IsRowVersion();


            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

