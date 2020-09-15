using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enumarations;
using Core.HttpClient;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Expenses;
using HaxTextile.WebAppCore.Models.Expense;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{
    public class ExpensesController : BaseController
    {
        private async Task<List<CustomerResponseDTO>> GetAllCustomerList()
        {
            var customerList = await _client.GetAsync<List<CustomerResponseDTO>>($"{baseApiUrl}/customer/all");
            return customerList;
        }
        private readonly IHttpClientWrapper _client;
        public ExpensesController(IHttpClientWrapper client)
        {
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            var searchDto = new ExpenseSearchRequestDTO();
            searchDto.CustomerList = new List<CustomerResponseDTO>();
            searchDto.CustomerList = await GetAllCustomerList();
            return View(searchDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateOrUpdateExpenseViewModel model)
        {
            if (ModelState.IsValid)
                return View(model);
            await _client.PutAsync<CreateOrUpdateExpenseViewModel, CreateOrUpdateExpenseViewModel>($"{baseApiUrl}/expense/{model.Id}", model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("Index", "Admin");

            var result = await _client.GetAsync<ExpenseResponseDTO>($"{baseApiUrl}/expense/{Id}");
            var customerList = await GetAllCustomerList();
            var viewModel = new CreateOrUpdateExpenseViewModel()
            {
                MethodType = "Update",
                ExpiryDate = result.ExpiryDate,
                Expiry = result.Expiry,
                CustomerId = result.CustomerId,
                Amount = result.Amount,
                Id = result.Id,
                CurrencyType = result.CurrencyType,
                Description = result.Description,
                DocumentNumber = result.DocumentNumber,
                Type = result.Type,
                CustomerList = customerList
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _client.PostAsync<CreateOrUpdateExpenseViewModel, CreateOrUpdateExpenseViewModel>($"{baseApiUrl}/expense", model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create(ExpenseType expenseType)
        {
            if (expenseType == 0)
                return RedirectToAction("Index", "Admin");

            var customerList = await GetAllCustomerList();

            var viewModel = new CreateOrUpdateExpenseViewModel()
            {
                MethodType = "Create",
                Type = expenseType,
                ExpiryDate = DateTime.Today,
                CustomerList = customerList
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ExpenseList(ExpenseSearchRequestDTO requestDTO)
        {
            var result = await _client.GetAsync<List<ExpenseResponseDTO>>($"{baseApiUrl}/expense?ExpenseType={requestDTO.ExpenseType}" +
                $"&CustomerId={requestDTO.CustomerId}&Description={requestDTO.Description}&DocumentNumber={requestDTO.DocumentNumber}" +
                $"&Expiry={requestDTO.Expiry}&ExpiryDate={requestDTO.ExpiryDate}&Start={requestDTO.Start}&Length={requestDTO.Length}");
            return new OkObjectResult(new { requestDTO.Draw, Expenses = result });
        }
    }
}
