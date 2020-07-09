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
            return new OkObjectResult(new { requestDTO.Draw, Customers = result });
        }
        public async Task<IActionResult> DeleteCustomer(Guid Id)
        {
            await _client.DeleteAsync($"/api/customer/{Id}");
            return new OkObjectResult(new { Id });
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateOrUpdateCustomerViewModel model = new CreateOrUpdateCustomerViewModel
            {
                MethodType = "Create"
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var result = await _client.GetAsync<CustomerResponseDTO>($"/api/customer/{Id}");
            CreateOrUpdateCustomerViewModel model = new CreateOrUpdateCustomerViewModel
            {
                MethodType = "Update",
                Id = result.Id,
                CreatedAt = result.CreatedAt,
                CustomerAddress = result.CustomerAddress,
                CustomerCompanyType = result.CustomerCompanyType,
                CustomerDescription = result.CustomerDescription,
                CustomerEmailAddress = result.CustomerEmailAddress,
                CustomerName = result.CustomerName,
                CustomerTelephoneNumber = result.CustomerTelephoneNumber
            };
            return View(model);
        }
    }
}
