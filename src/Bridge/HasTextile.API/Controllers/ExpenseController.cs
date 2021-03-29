using AutoMapper;
using Domain.Model.Income;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Expenses.Model;
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
        /// Return an expense with Unique Id
        /// </summary>
        /// <param name="Id">Unique Expense Id</param>
        /// <returns>An Expense</returns>
        /// <response code="200">Expense DTO</response>
        /// <response code="404">Not Found</response>
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
        /// Passivate an Expense
        /// </summary>
        /// <param name="Id">Unique Expense Id</param>
        /// <returns>200 OK</returns>
        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeActivateExpense(Guid Id)
        {
            await _expensesService.PassivateExpense(Id);
            return Ok();
        }
        /// <summary>
        /// Create New Expense
        /// </summary>
        /// <param name="requestDTO">New Expense request payload.</param>
        /// <returns>New Expense's Id</returns>
        /// <response code="200">Return Expense's Guid Id</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateNewExpense([FromBody] ExpenseRequestDTO requestDTO)
        {
            var result = await _expensesService.CreateNewExpense(requestDTO);
            return new OkObjectResult(new { Id = result });
        }
        /// <summary>
        /// Return Filtered Expense list.
        /// </summary>
        /// <param name="requestDTO">Query String Request</param>
        /// <returns>List of Expenses</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseResponseDTO>))]
        public async Task<IActionResult> FilterExpenses([FromQuery] ExpenseFilterRequestDTO requestDTO)
        {
            var result = await _expensesService.GetExpensesWithFilter(requestDTO);
            var mapResult = _mapper.Map<List<Expenses>, List<ExpenseResponseDTO>>(result);
            return new OkObjectResult(mapResult);
        }
        /// <summary>
        /// Update an Expense
        /// </summary>
        /// <param name="Id">Expense Unique Id</param>
        /// <param name="requestDTO">Request Payload</param>
        /// <returns>Return Expense Id</returns>
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> UpdateExpense(Guid Id, [FromBody] ExpenseRequestDTO requestDTO)
        {
            var result = await _expensesService.UpdateExpense(Id, requestDTO);
            return new OkObjectResult(new { Id = result });
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> FindSector(Guid Id)
        {
            var result = await _expensesService.CreateSector
        }
    }
}
