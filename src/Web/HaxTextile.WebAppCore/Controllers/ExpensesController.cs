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
        [HttpGet]
        public IActionResult Create(ExpenseType expenseType)
        {
            if (expenseType == 0)
                return RedirectToAction("Index", "Admin");
            return View();
        }
        [HttpGet]
        public IActionResult ExpenseList(ExpenseSearchRequestDTO requestDTO)
        {
            List<ExpenseResponseDTO> k = new List<ExpenseResponseDTO>();
            return new OkObjectResult(new { requestDTO.Draw, Expenses = k });
        }
    }
}
