using Domain.AuditLoggs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<UserLoginAuditLog>
    {
        public void Configure(EntityTypeBuilder<UserLoginAuditLog> builder)
        {
            builder.ToTable("UserLoginAuditLog");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);

            builder.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();

            builder.Property(x => x.Action)
                .IsRequired();

            builder.Property(x => x.IpAddress)
                .HasMaxLength(45);

            builder.Property(x => x.UserAgent)
                .HasMaxLength(500);

            builder.Property(x => x.IsSuccess)
                   .IsRequired();

            builder.Property(x => x.ErrorMessage)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.HasIndex(x => x.UserId);

           
        }
    }
}
