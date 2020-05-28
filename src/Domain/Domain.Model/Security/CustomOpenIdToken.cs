using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Security
{
    public class CustomOpenIdToken : OpenIddictToken<Guid, CustomOpenIdApplication, CustomOpenIdAuthorization>
    {
        public CustomOpenIdToken()
        {
            Id = Guid.NewGuid();
        }
    }
}
