using AutoMapper;
using Core.Caching;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Customer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HasTextile.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    //[Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        //private readonly ICustomerService _customerService;
        //private readonly IMapper _mapper;
        //private readonly CacheProvider _cacheProvider;
        //private const string cacheName = "customerCacheName";
        //public CustomerController(ICustomerService customerService, IMapper mapper, CacheProvider cacheProvider)
        //{
        //    _customerService = customerService;
        //    _mapper = mapper;
        //    _cacheProvider = cacheProvider;
        //}
        ///// <summary>
        ///// Returns all active customers.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("all")]
        //[ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        //public async Task<IActionResult> FindAllCustomer()
        //{
        //    var cacheResult = await _cacheProvider.GetOrAddAsync("getAllCustomer", cacheName, TimeSpan.FromMinutes(15), Core.Enumarations.ExpirationMode.Absolute, async () =>
        //    {
        //        return await _customerService.GetAllCustomerAsync();
        //    });
        //    var result = _mapper.Map<List<Customer>, List<CustomerResponseDTO>>(cacheResult);
        //    return new OkObjectResult(result);
        //}
        ///// <summary>
        ///// Return customer's expense list
        ///// </summary>
        ///// <param name="Id">Customer Unique Id</param>
        ///// <returns>Return customer's info with expense list.</returns>
        ///// <response code="200">Return Customer Info</response>
        ///// <response code="404">Not found customer.</response>
        //[HttpGet("{Id:guid}/Expenses")]
        //[ProducesResponseType(typeof(CustomerResponseDTO), 200)]
        //[ProducesResponseType(typeof(NotFoundResult), 404)]
        //public async Task<IActionResult> FindExpenses(Guid Id)
        //{
        //    var instance = await _customerService.GetCustomerExpensesAsync(Id);
        //    if (instance == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    var result = _mapper.Map<Customer, CustomerResponseDTO>(instance);
        //    return new OkObjectResult(result);
        //}
        ///// <summary>
        ///// Find Customer without expenses.
        ///// </summary>
        ///// <param name="Id">Unique Id</param>
        ///// <returns></returns>
        //[HttpGet("{Id:guid}")]
        //[ProducesResponseType(typeof(CustomerResponseDTO), 200)]
        //[ProducesResponseType(typeof(NotFoundResult), 404)]
        //public async Task<IActionResult> FindCustomer(Guid Id)
        //{
        //    var instance = await _customerService.GetCustomerAsync(Id);
        //    if (instance == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    var result = _mapper.Map<Customer, CustomerResponseDTO>(instance);
        //    return new OkObjectResult(result);
        //}
        ///// <summary>
        ///// Filterize customer with some properties.
        ///// </summary>
        ///// <param name="request">QueryString </param>
        ///// <returns></returns>
        //[HttpGet]
        //[ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        //public async Task<IActionResult> FilterCustomers([FromQuery] CustomerFilterRequestDTO request)
        //{
        //    var resultList = await _customerService.GetCustomersWithFilter(request);
        //    if (resultList == null)
        //    {
        //        return new OkObjectResult(new List<CustomerResponseDTO>());
        //    }
        //    var instanceList = _mapper.Map<List<Customer>, List<CustomerResponseDTO>>(resultList);
        //    return new OkObjectResult(instanceList);
        //}
        ///// <summary>
        ///// Passivate customer.
        ///// </summary>
        ///// <param name="Id">Customer's Unique Id</param>
        ///// <returns></returns>
        //[HttpDelete("{Id:guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> DeActivateCustomer(Guid Id)
        //{
        //    await _customerService.PassivateCustomer(Id);
        //    await DeleteCache("getAllCustomer", cacheName);
        //    return Ok();
        //}
        ///// <summary>
        ///// Update customer
        ///// </summary>
        ///// <param name="Id">Unique customer Id</param>
        ///// <param name="request">Request Payload</param>
        ///// <returns>Customer's Id</returns>
        //[HttpPut("{Id:guid}")]
        //[ProducesResponseType(200, Type = typeof(Guid))]
        //public async Task<IActionResult> UpdateCustomer(Guid Id, [FromBody] CustomerRequestDTO request)
        //{
        //    await _customerService.UpdateCustomer(Id, request);
        //    if (Id != null && Id != Guid.Empty)
        //        await DeleteCache("getAllCustomer", cacheName);

        //    return new OkObjectResult(new { Id });
        //}
        ///// <summary>
        ///// create a new customer.
        ///// </summary>
        ///// <param name="request">customer request payload</param>
        ///// <returns>New Customer's Id</returns>
        //[HttpPost]
        //[ProducesResponseType(200, Type = typeof(Guid))]
        //public async Task<IActionResult> CreateNewUser([FromBody] CustomerRequestDTO request)
        //{
        //    var Id = await _customerService.CreateNewCustomer(request);
        //    if (Id != null)
        //        await DeleteCache("getAllCustomer", cacheName);
        //    return new OkObjectResult(new { Id });
        //}
        //private async Task DeleteCache(string cacheKey, string cacheName)
        //{
        //    await _cacheProvider.RemoveAsync(cacheKey, cacheName);
        //}
    }
}
