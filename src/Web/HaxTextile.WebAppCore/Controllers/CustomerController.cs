using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.HttpClient;
using HaxTextile.WebAppCore.Models;
using HaxTextile.WebAppCore.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IHttpClientWrapper _client;
        public CustomerController(IHttpClientWrapper httpClient)
        {
            _client = httpClient;
        }
        public IActionResult Index()
        {
            CustomerSearchRequestDTO model = new CustomerSearchRequestDTO();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerList(CustomerSearchRequestDTO requestDTO)
        {
            var result = await _client.GetAsync<JsonResult>("/api/customer/60F4C877-25A4-4C39-98BB-ECC501D2C7AA");
            return result;
        }
    }
}
