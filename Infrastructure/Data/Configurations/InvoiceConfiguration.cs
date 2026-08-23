using Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>().IsRequired();
            builder.Property(x => x.InvoiceType).HasConversion<int>().IsRequired();
            //builder.Property(x => x.NetAmount).HasColumnType("decimal(18,2)").IsRequired();
            //builder.Property(x => x.SubTotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            //builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

            builder.HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

