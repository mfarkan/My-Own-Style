using Core.HttpClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    [Authorize(Roles = "HasTextileSystemAdmin")]
    public class AdminController : BaseController
    {
        private readonly IHttpClientWrapper _client;
        public AdminController(IHttpClientWrapper client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
