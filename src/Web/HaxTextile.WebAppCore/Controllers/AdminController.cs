using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.HttpClient;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    public class AdminController : BaseAdminController
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
