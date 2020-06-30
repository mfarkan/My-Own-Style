using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    public class CustomerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
