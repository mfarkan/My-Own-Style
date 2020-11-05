using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class SecurityExtensions
    {
        public static string GetUserInstitutionId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(q => q.Type == "institutionId");
            return claim?.Value;
        }
    }
}
