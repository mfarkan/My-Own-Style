using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Security.IdentityServer.Models;

namespace Security.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                ReturnUrl = string.Empty,
                ExternalLogins = null,
            };
            return View(model);
        }
    }
}
