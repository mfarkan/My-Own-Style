using Core.Enumarations;
using Domain.Model.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataLayer
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
                entity.Property(q => q.CustomerAddress).IsRequired().HasMaxLength(255);
                entity.Property(q => q.CustomerCompanyType).IsRequired().HasMaxLength(255);
                entity.Property(q => q.CustomerDescription).IsRequired().HasMaxLength(255);
                entity.Property(q => q.CustomerEmailAddress).IsRequired().HasMaxLength(255);
                entity.Property(q => q.CustomerName).IsRequired().HasMaxLength(255);
                entity.Property(q => q.CustomerTelephoneNumber).HasMaxLength(50);
            });
        }
    }
}
