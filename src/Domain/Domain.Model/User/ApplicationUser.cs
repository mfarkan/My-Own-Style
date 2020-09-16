using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Model.User
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //Kurum özelinde kullanıcıların olması gerekiyor ve ayrıca admin kullanıcısının da olması gerekiyor.
        public ApplicationUser()
        {

        }
        public ApplicationUser(string userName) : base(userName)
        {

        }
    }
}
