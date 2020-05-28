using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Security
{
    public class CustomOpenIdScope : OpenIddictScope<Guid>
    {
        public CustomOpenIdScope()
        {
            Id = Guid.NewGuid();
        }
    }
}
