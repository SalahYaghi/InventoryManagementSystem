using Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class LineItemConfiguration : IEntityTypeConfiguration<InvoiceLineItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
        {
            builder.ToTable("InvoiceLineItems", t =>
                t.HasCheckConstraint("CK_LineItems_LineNo_NonNegative", "[LineNo] >= 0"));

            builder.HasKey(x => new {x.LineNo, x.InvoiceId});
            builder.Property(x => x.LineNo).IsRequired();

            builder.Property(x => x.Tax).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.InvoiceId).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne<Invoice>()
                .WithMany(p => p.LineItems)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

