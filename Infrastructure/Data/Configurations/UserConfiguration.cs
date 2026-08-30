using Domain.Identity.Users;
using Inventory.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            
            builder.Property(x => x.IsActive)
                   .IsRequired();
          
            builder.Property(x => x.EmployeeId)
                   .IsRequired();

            builder.HasOne(e => e.Employee)
                   .WithMany(e => e.Users)
                   .HasForeignKey(u => u.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(); 

            builder.Property(x => x.LastLoginAt)
                .IsRequired();

            builder.Property(x => x.Username)
                    .IsRequired();
            builder.Property(x => x.Email)
                    .IsRequired();

            builder.Property(e => e.HashedPassword)
                   .HasColumnType("NVARCHAR") 
                   .HasMaxLength(500)
                   .IsRequired(); 

            builder.HasIndex(x => x.Username).IsUnique();

            builder.ToTable(t => {
                t.HasCheckConstraint("CK_User_Username_MinLength",
                    $"LEN(Username) >= {UserRules.UsernameMinLength} and LEN(Username) <= {UserRules.UsernameMaxLength}");


            });
        }
    }
}

