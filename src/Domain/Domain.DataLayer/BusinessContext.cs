using Core.Enumarations;
using Domain.Model.Customer;
using Domain.Model.Income;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataLayer
{
    public class BusinessContext : DbContext
    {
        // we're using Repository pattern because of that we don't need dbSet properties. 
        //If we'll use dbContext directly into code we should add these properties.
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
            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.ToTable("Expenses");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
                entity.Property(q => q.Description).HasMaxLength(255);
                entity.Property(q => q.Amount).HasColumnType("decimal(18,2)").IsRequired(true);
                entity.Property(q => q.DocumentNumber).HasMaxLength(50);
            });
        }
    }
}
