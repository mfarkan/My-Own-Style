using Domain.Model.Customer;
using Domain.Service.Model.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        public async Task<IActionResult> FindCustomer(Guid Id)
        {
            var result = await _customerService.GetCustomerAsync(Id);
            return new OkObjectResult(result);
        }
    }
}
