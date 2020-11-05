using Domain.Model.Institution;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Model.User
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual Institution.Institution Institution { get; set; }
        //Kurum özelinde kullanıcıların ve rollerin olması gerekiyor.
        public ApplicationUser()
        {

        }
        public ApplicationUser(string userName) : base(userName)
        {

        }
    }
}
