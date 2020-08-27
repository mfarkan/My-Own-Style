using AutoMapper;
using Domain.Model.Income;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Expenses.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace HasTextile.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    //[Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpensesService _expensesService;
        public ExpenseController(IMapper mapper, IExpensesService expensesService)
        {
            _mapper = mapper;
            _expensesService = expensesService;
        }
        /// <summary>
        /// Spesifik olarak belirtilen gelir/gider bilgisini döner.
        /// </summary>
        /// <param name="Id">Gelir/Gider Id Bilgisi</param>
        /// <returns></returns>
        /// <response code="200">Gelir/Gider bilgisi</response>
        /// <response code="404">Boş Result</response>
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(ExpenseResponseDTO), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> FindExpense(Guid Id)
        {
            var result = await _expensesService.GetExpense(Id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            var instance = _mapper.Map<Expenses, ExpenseResponseDTO>(result);
            return new OkObjectResult(instance);
        }
        /// <summary>
        /// Bir gelir gider bilgisini pasivize eder.
        /// </summary>
        /// <param name="Id">Gelir/Gider Id bilgisi</param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeActivateExpense(Guid Id)
        {
            await _expensesService.PassivateExpense(Id);
            return Ok();
        }
        /// <summary>
        /// Yeni gelir/gider bilgisi kaydı oluşturmak için kullanılır.
        /// </summary>
        /// <param name="requestDTO">Yeni gelir/gider oluşturmak için kullanılan request payload.</param>
        /// <returns>Kayıt oluşturulursa Id bilgisi geri döner</returns>
        /// <response code="200">Başarılı kayıt işlemi. GUID döner.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateNewExpense([FromBody] ExpenseRequestDTO requestDTO)
        {
            var result = await _expensesService.CreateNewExpense(requestDTO);
            return new OkObjectResult(new { Id = result });
        }
        /// <summary>
        /// Belirli bir filtreleme parameterlerine göre gelir/gider bilgisini döner.
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        /// <response code="200">Başarılı sorgulama işlemi.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseResponseDTO>))]
        public async Task<IActionResult> FilterExpenses([FromQuery] ExpenseFilterRequestDTO requestDTO)
        {
            var result = await _expensesService.GetExpensesWithFilter(requestDTO);
            var mapResult = _mapper.Map<List<Expenses>, List<ExpenseResponseDTO>>(result);
            return new OkObjectResult(mapResult);
        }
        /// <summary>
        /// Belirli bir Id bilgisi verilen Gelir/Gider'in güncelleme işlemine tabi tutulması.
        /// </summary>
        /// <param name="Id">Gelir/Gider bilgisinin Id bilgisi.</param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> UpdateExpense(Guid Id, [FromBody] ExpenseRequestDTO requestDTO)
        {
            var result = await _expensesService.UpdateExpense(Id, requestDTO);
            return new OkObjectResult(new { Id = result });
        }
    }
}
