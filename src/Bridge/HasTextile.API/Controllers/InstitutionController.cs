using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Core.Caching;
using Domain.Model.Institution;
using Domain.Service.Model.Institution;
using Domain.Service.Model.Institution.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HasTextile.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;
        private readonly CacheProvider _cacheProvider;
        private readonly IMapper _mapper;
        private const string cacheName = "InstitutionCacheName";
        public InstitutionController(IInstitutionService institutionService, CacheProvider cacheProvider, IMapper mapper)
        {
            _institutionService = institutionService;
            _cacheProvider = cacheProvider;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns all Institutions
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<InstitutionResponseDTO>), 200)]
        public async Task<IActionResult> FindAllInstitutions()
        {
            var cacheResult = await _cacheProvider.GetOrAddAsync("getAllInstitution", cacheName, TimeSpan.FromMinutes(15), Core.Enumarations.ExpirationMode.Absolute, async () =>
            {
                return await _institutionService.GetAllInstitutionsAsync();
            });
            var result = _mapper.Map<List<Institution>, List<InstitutionResponseDTO>>(cacheResult);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Return a Institution with Id
        /// </summary>
        /// <param name="Id">Institution Unique Id</param>
        /// <returns see="200">OK</returns>
        /// <returns see="404">Not Found</returns>
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(InstitutionResponseDTO), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> FindInstitution(Guid Id)
        {
            var instance = await _institutionService.GetInstitutionAsync(Id);
            if (instance == null)
            {
                return new NotFoundResult();
            }
            var result = _mapper.Map<Institution, InstitutionResponseDTO>(instance);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Return Filtered List of Institution
        /// </summary>
        /// <param name="requestDTO">request Payload</param>
        /// <returns see="200">Ok</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<InstitutionResponseDTO>), 200)]
        public async Task<IActionResult> FindInstitutionsWithFilter([FromQuery] InstitutionFilterRequestDTO requestDTO)
        {
            var resultList = await _institutionService.GetInstitutionsWithFilterAsync(requestDTO);
            if (resultList == null)
            {
                return new OkObjectResult(new List<InstitutionResponseDTO>());
            }
            var instanceList = _mapper.Map<List<Institution>, List<InstitutionResponseDTO>>(resultList);
            return new OkObjectResult(instanceList);
        }
        /// <summary>
        /// Passivate Institution
        /// </summary>
        /// <param name="Id">Institution Unique Id</param>
        /// <returns see="200">OK</returns>
        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeActivateInstitution(Guid Id)
        {
            await _institutionService.DeleteAsync(Id);
            await DeleteCache("getAllInstitution", cacheName);
            return new OkObjectResult(new { Id });
        }
        /// <summary>
        /// Institution Update
        /// </summary>
        /// <param name="Id">Institution Unique Id</param>
        /// <param name="requestDTO">Request PayLoad</param>
        /// <returns></returns>
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> UpdateInstitution(Guid Id, [FromBody] InstitutionRequestDTO requestDTO)
        {
            await _institutionService.UpdateAsync(Id, requestDTO);
            if (Id != null && Id != Guid.Empty)
                await DeleteCache("getAllInstitution", cacheName);
            return new OkObjectResult(new { Id });
        }
        /// <summary>
        /// Create a new Institution
        /// </summary>
        /// <param name="requestDTO">Request payload</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateNewInstitution([FromBody] InstitutionRequestDTO requestDTO)
        {
            var result = await _institutionService.CreateAsync(requestDTO);
            if (result != null)
                await DeleteCache("getAllInstitution", cacheName);
            return new OkObjectResult(new { result });
        }

        private async Task DeleteCache(string cacheKey, string cacheName)
        {
            await _cacheProvider.RemoveAsync(cacheKey, cacheName);
        }
    }
}
