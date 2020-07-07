using Core.Enumarations;
using Domain.Model.Customer;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Customer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HasTextile.API.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(CustomerResponseDTO), 200)]
        public async Task<IActionResult> FindCustomer(Guid Id)
        {
            var result = await _customerService.GetCustomerAsync(Id);
            return new OkObjectResult(result);
        }
        //[HttpGet("{start:int}/{length:int}")]
        //[ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        //[ProducesResponseType(typeof(void), 404)]
        //public async Task<IActionResult> CustomerList(int start, int length)
        //{
        //    var resultList = await _customerService.GetCustomersAsync(start, length);
        //    return new OkObjectResult(resultList);
        //}
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        public async Task<IActionResult> FilterCustomers([FromQuery] CustomerFilterRequestDTO request)
        {
            var resultList = await _customerService.GetCustomersWithFilter(request.CustomerName, request.CustomerAddress, request.CustomerPhoneNumber,
                request.CustomerEmail, request.CustomerType, request.Start, request.Length);
            return new OkObjectResult(resultList);
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeActivateCustomer(Guid Id)
        {
            await _customerService.PassivateCustomer(Id);
            return Ok();
        }
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid Id, [FromBody] CustomerRequestDTO request)
        {
            var result = await _customerService.UpdateCustomer(Id, request);
            return new OkObjectResult(new { Id = result });
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] CustomerRequestDTO request)
        {
            var result = await _customerService.CreateNewCustomer(request);
            return new OkObjectResult(new { Id = result });
        }
    }
}
