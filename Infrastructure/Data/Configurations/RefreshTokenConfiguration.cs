using Domain.Identity.RefreshToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .IsRequired();

            builder.Property(r => r.ExpiresAt)
                .HasColumnType("DateTimeOffset")
                   .IsRequired();

            builder.Property(r => r.RevokedAt)
                .HasColumnType("DateTimeOffset")
                   .IsRequired();
            builder.Property(r => r.RevokedAt)
                   .IsRequired(false);

            builder.ToTable("RefreshTokens");
        }
    }
}

