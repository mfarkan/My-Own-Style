using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    [Authorize(Roles = "HasTextileSystemAdmin")]
    public class InstitutionController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(Guid Id)
        {
            return View();
        }
        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            return View();
        }
    }
}
