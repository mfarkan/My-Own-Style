using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.HttpClient;
using Domain.Service.Model.Customer;
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
            var result = await _client.GetAsync<List<CustomerResponseDTO>>($"/api/customer?CustomerName={requestDTO.CustomerName}" +
                $"&CustomerAddress={requestDTO.CustomerAddress}&CustomerEmail={requestDTO.CustomerEmail}&CustomerPhoneNumber={requestDTO.CustomerPhoneNumber}" +
                $"&CustomerType={requestDTO.CustomerType}&Start={requestDTO.Start}&Length={requestDTO.Length}");
            return new OkObjectResult(result);
        }
    }
}
