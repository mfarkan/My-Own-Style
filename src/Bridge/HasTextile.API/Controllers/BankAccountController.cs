using AutoMapper;
using Core.Caching;
using Domain.Service.Model.BankAccount;
using Domain.Service.Model.BankAccount.Model;
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
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        private readonly CacheProvider _cacheProvider;
        private const string cacheName = "customerCacheName";
        public BankAccountController(IBankAccountService bankAccountService, CacheProvider cacheProvider, IMapper mapper)
        {
            _bankAccountService = bankAccountService;
            _cacheProvider = cacheProvider;
            _mapper = mapper;
        }
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BankAccountResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> FindBankAccount(Guid Id)
        {
            var result = await _bankAccountService.GetBankAccountAsync(Id);
            if (result == null)
                return NotFound();

            var instance = _mapper.Map<Domain.Model.Account.BankAccount, BankAccountResponseDTO>(result);
            return new OkObjectResult(instance);
        }
        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> DeleteBankAccount(Guid Id)
        {
            await _bankAccountService.PassivateBankAccountAsync(Id);
            return Ok();
        }
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> UpdateBankAccount(Guid Id, [FromBody] BankAccountRequestDTO requestDTO)
        {
            var result = await _bankAccountService.UpdateBankAccountAsync(Id, requestDTO);
            return new OkObjectResult(new { Id = result });
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountRequestDTO requestDTO)
        {
            var result = await _bankAccountService.CreateNewBankAccountAsync(requestDTO);
            return new OkObjectResult(new { Id = result });

        }
    }
}
