using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Model.User
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual Institution.Institution Institution { get; set; }
        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName) : base(roleName)
        {

        }
    }
}
