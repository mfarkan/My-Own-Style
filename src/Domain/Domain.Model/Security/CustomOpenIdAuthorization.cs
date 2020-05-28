using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Security
{
    public class CustomOpenIdAuthorization : OpenIddictAuthorization<Guid, CustomOpenIdApplication, CustomOpenIdToken>
    {
        public CustomOpenIdAuthorization()
        {
            Id = Guid.NewGuid();
        }
    }
}
