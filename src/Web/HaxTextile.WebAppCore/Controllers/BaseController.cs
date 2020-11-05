using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaxTextile.WebAppCore.Controllers
{

    //[Authorize]
    public abstract class BaseController : Controller
    {
        protected const string baseApiUrl = "/api/v1.0";
        protected async Task<Dictionary<string, string>> GetDefaultHeaders()
        {
            var headers = new Dictionary<string, string>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            headers.Add("Authorization", "Bearer " + accessToken);
            return headers;
        }
    }
}
