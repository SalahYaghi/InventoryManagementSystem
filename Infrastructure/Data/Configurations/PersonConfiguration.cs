using Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NationalNo).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(10).IsRequired();
            builder.Property(x => x.SecondName).HasMaxLength(10).IsRequired();
            builder.Property(x => x.ThirdName).HasMaxLength(10);
            builder.Property(x => x.LastName).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(500);

            builder.Property(x => x.ContactId).IsRequired();
            builder.Property(x => x.AddressId).IsRequired();
            builder.Property(x => x.DocumentId).IsRequired(false);

            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedUtc).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);

            builder.HasOne(x => x.Contact)
                .WithMany()
                .HasForeignKey(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict )
                .IsRequired();

            builder.HasOne(x => x.Document)
                .WithMany()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.SetNull )
                .IsRequired(false);

            builder.HasIndex(x => x.NationalNo).IsUnique();
        }
    }
}

