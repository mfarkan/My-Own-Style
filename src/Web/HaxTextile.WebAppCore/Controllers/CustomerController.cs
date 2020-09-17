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
            var result = await _client.GetAsync<List<CustomerResponseDTO>>($"{baseApiUrl}/customer?CustomerName={requestDTO.CustomerName}" +
                $"&CustomerAddress={requestDTO.CustomerAddress}&CustomerEmail={requestDTO.CustomerEmail}&CustomerPhoneNumber={requestDTO.CustomerPhoneNumber}" +
                $"&CustomerType={requestDTO.CustomerType}&Start={requestDTO.Start}&Length={requestDTO.Length}");
            return new OkObjectResult(new { requestDTO.Draw, Customers = result });
        }
        public async Task<IActionResult> DeleteCustomer(Guid Id)
        {
            await _client.DeleteAsync($"/api/customer/{Id}");
            return new OkObjectResult(new { Id });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _client.PostAsync<CreateOrUpdateCustomerViewModel, CreateOrUpdateCustomerViewModel>($"/api/customer", model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateOrUpdateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _client.PutAsync<CreateOrUpdateCustomerViewModel, CreateOrUpdateCustomerViewModel>($"{baseApiUrl}/customer/{model.Id}", model);
            return RedirectToAction("Index");
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
            var result = await _client.GetAsync<CustomerResponseDTO>($"{baseApiUrl}/customer/{Id}");
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
