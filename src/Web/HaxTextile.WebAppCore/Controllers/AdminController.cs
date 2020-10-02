using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.HttpClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    [Authorize(Roles = "HasTextileSystemAdmin")]
    public class AdminController : BaseAdminController
    {
        // sol taraftaki menüyü admin rolüne göre yapabiliriz.
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
