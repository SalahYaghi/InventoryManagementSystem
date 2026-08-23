using Domain.Identity.Employee;
using Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Person)
                   .WithOne()
                   .HasForeignKey<Employee>(e => e.PersonId)
                   .OnDelete(DeleteBehavior.Restrict)    
                   .IsRequired();

            builder.Property(e => e.HiringDate)
                   .IsRequired();

            builder.Property(e => e.JobTitle)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(b => b.Warehouse)
                   .WithMany(b => b.Employees)
                   .HasForeignKey(w => w.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);

                  
        }
    }
}

