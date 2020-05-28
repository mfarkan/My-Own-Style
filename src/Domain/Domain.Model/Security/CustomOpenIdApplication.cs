using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Security
{
    public class CustomOpenIdApplication : OpenIddictApplication<Guid, CustomOpenIdAuthorization, CustomOpenIdToken>
    {
        public CustomOpenIdApplication()
        {
            Id = Guid.NewGuid();
        }
    }
}
