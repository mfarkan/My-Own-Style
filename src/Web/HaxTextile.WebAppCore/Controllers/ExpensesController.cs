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
        private readonly IHttpClientWrapper _client;
        public ExpensesController(IHttpClientWrapper client)
        {
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            var searchDto = new ExpenseSearchRequestDTO();
            searchDto.CustomerList = new List<CustomerResponseDTO>();
            //var customerList = await _client.GetAsync<List<CustomerResponseDTO>>($"{baseApiUrl}/customer/all");
            //searchDto.CustomerList = customerList;
            return View(searchDto);
        }
        [HttpPost]
        public IActionResult Update(CreateOrUpdateExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
                return RedirectToAction("Index", "Admin");

            var viewModel = new CreateOrUpdateExpenseViewModel()
            {
                MethodType = "Update",
                ExpiryDate = DateTime.Today,
                CustomerList = new List<CustomerResponseDTO>()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(CreateOrUpdateExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(ExpenseType expenseType)
        {
            if (expenseType == 0)
                return RedirectToAction("Index", "Admin");

            var viewModel = new CreateOrUpdateExpenseViewModel()
            {
                MethodType = "Create",
                ExpenseType = expenseType,
                ExpiryDate = DateTime.Today,
                CustomerList = new List<CustomerResponseDTO>()
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ExpenseList(ExpenseSearchRequestDTO requestDTO)
        {
            List<ExpenseResponseDTO> k = new List<ExpenseResponseDTO>();
            return new OkObjectResult(new { requestDTO.Draw, Expenses = k });
        }
    }
}
