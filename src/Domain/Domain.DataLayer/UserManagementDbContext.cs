using Domain.Model.Security;
using Domain.Model.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataLayer
{
    public class UserManagementDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseOpenIddict<CustomOpenIdApplication, CustomOpenIdAuthorization, CustomOpenIdScope, CustomOpenIdToken, Guid>();
        }
    }
}
