using Core.Enumarations;
using Domain.Model.Income;
using Domain.Model.Institution;
using Domain.Model.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domain.DataLayer
{
    public class ManagementDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ManagementDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
            builder.Entity<Domain.Model.Sector.Sector>(entity =>
            {
                entity.ToTable("Sectors");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.SectorDescription).IsRequired().HasMaxLength(50);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
            });
            builder.Entity<Domain.Model.Account.BankAccount>(entity =>
            {
                entity.ToTable("BankAccount");
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
                entity.Property(q => q.BankAccountDescription).IsRequired().HasMaxLength(100);
                entity.Property(q => q.BankAccountName).IsRequired().HasMaxLength(50);
                entity.Property(q => q.AccountIBAN).HasMaxLength(34);
            });
            builder.Entity<Expenses>(entity =>
            {
                entity.ToTable("Expenses");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
                entity.Property(q => q.Description).HasMaxLength(255);
                entity.Property(q => q.Amount).HasColumnType("decimal(18,2)").IsRequired(true);
                entity.Property(q => q.DocumentNumber).HasMaxLength(50);
            });
            builder.Entity<Institution>(entity =>
            {
                entity.ToTable("Institution");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
                entity.Property(q => q.EmailAddress).IsRequired().HasMaxLength(255);
                entity.Property(q => q.Name).IsRequired().HasMaxLength(255);
                entity.Property(q => q.PhoneNumber).HasMaxLength(50);
                entity.Property(q => q.Code).IsRequired().HasMaxLength(255);
            });
            builder.Entity<InstitutionSettings>(entity =>
            {
                entity.ToTable("InstitutionSettings");
                entity.HasIndex(q => q.CreatedAt);
                entity.Property(q => q.Status).HasDefaultValue(StatusType.Active);
            });
        }
    }
}
