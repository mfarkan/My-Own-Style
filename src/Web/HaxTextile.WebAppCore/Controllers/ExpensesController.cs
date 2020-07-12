using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enumarations;
using Core.HttpClient;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    public class ExpensesController : BaseController
    {
        private readonly IHttpClientWrapper _client;
        public ExpensesController(IHttpClientWrapper client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(ExpenseType expenseType)
        {
            if (expenseType == 0)
                return RedirectToAction("Index", "Admin");
            return View();
        }
    }
}
