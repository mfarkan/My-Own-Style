using AutoMapper;
using Domain.Model.Customer;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    //[Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Sistemdeki tüm aktif müşterileri döner.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        public async Task<IActionResult> FindAllCustomer()
        {
            var customerList = await _customerService.GetAllCustomerAsync();
            var result = _mapper.Map<List<Customer>, List<CustomerResponseDTO>>(customerList);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Müşteriye ait gelir/gider bilgilerini dönen servis
        /// </summary>
        /// <param name="Id">Müşterinin Id Bilgisi</param>
        /// <returns>Müşterinin kendisini gelir/gider bilgileriyle döner.</returns>
        /// <response code="200">müşteri bilgisi döner.</response>
        /// <response code="404">Id değeri için müşteri bulunamadı döner.</response>
        [HttpGet("{Id:guid}/Expenses")]
        [ProducesResponseType(typeof(CustomerResponseDTO), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> FindExpenses(Guid Id)
        {
            var instance = await _customerService.GetCustomerExpensesAsync(Id);
            if (instance == null)
            {
                return new NotFoundResult();
            }
            var result = _mapper.Map<Customer, CustomerResponseDTO>(instance);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Spesifik olarak Id'si verilen müşteri bilgisini döner.
        /// </summary>
        /// <param name="Id">Müşterinin Unique Id bilgisi</param>
        /// <returns></returns>
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(CustomerResponseDTO), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> FindCustomer(Guid Id)
        {
            var instance = await _customerService.GetCustomerAsync(Id);
            if (instance == null)
            {
                return new NotFoundResult();
            }
            var result = _mapper.Map<Customer, CustomerResponseDTO>(instance);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Müşterinin bazı bilgilerine göre filtrelenmesini sağlar.
        /// </summary>
        /// <param name="request">Query'den gelen request objesi.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerResponseDTO>), 200)]
        public async Task<IActionResult> FilterCustomers([FromQuery] CustomerFilterRequestDTO request)
        {
            var resultList = await _customerService.GetCustomersWithFilter(request);
            if (resultList == null)
            {
                return new OkObjectResult(new List<CustomerResponseDTO>());
            }
            var instanceList = _mapper.Map<List<Customer>, List<CustomerResponseDTO>>(resultList);
            return new OkObjectResult(instanceList);
        }
        /// <summary>
        /// Müşteriyi pasivize eder.
        /// </summary>
        /// <param name="Id">Müşterinin Unique Id bilgisi</param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeActivateCustomer(Guid Id)
        {
            await _customerService.PassivateCustomer(Id);
            return Ok();
        }
        /// <summary>
        /// Müşteriyi günceller.
        /// </summary>
        /// <param name="Id">Müşterinin Unique Id bilgisi</param>
        /// <param name="request">Müşterinin güncellenmiş datalarının bulunduğu request Objesi</param>
        /// <returns></returns>
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> UpdateCustomer(Guid Id, [FromBody] CustomerRequestDTO request)
        {
            await _customerService.UpdateCustomer(Id, request);
            return new OkObjectResult(new { Id });
        }
        /// <summary>
        /// Yeni müşteri oluşturmak için kullanılır.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> CreateNewUser([FromBody] CustomerRequestDTO request)
        {
            var Id = await _customerService.CreateNewCustomer(request);
            return new OkObjectResult(new { Id });
        }
    }
}
