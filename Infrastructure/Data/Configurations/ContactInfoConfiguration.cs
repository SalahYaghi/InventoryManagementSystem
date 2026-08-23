using Domain.Contacts.ContactInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.AlternitavePhoneNumber).HasMaxLength(20);
            builder.Property(x => x.FaxNumber).HasMaxLength(20);
            builder.Property(x => x.WebsiteUrl).HasMaxLength(500);

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        }
    }
}

