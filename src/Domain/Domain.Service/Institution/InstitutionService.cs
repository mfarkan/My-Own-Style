using Domain.DataLayer.Business;
using Domain.Service.Model.Institution;
using Domain.Service.Model.Institution.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Service.Institution
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IBusinessRepository _repository;
        public InstitutionService(IBusinessRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _repository.PassivateEntityAsync<Domain.Model.Institution.Institution>(Id);
            await Task.CompletedTask;
        }

        public async Task<List<Domain.Model.Institution.Institution>> GetAllInstitutionsAsync()
        {
            return await _repository.GetAllAsync<Domain.Model.Institution.Institution>();
        }

        public async Task<Domain.Model.Institution.Institution> GetInstitutionAsync(Guid Id)
        {
            return await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(Id);
        }

        public async Task<List<Domain.Model.Institution.Institution>> GetInstitutionsWithFilterAsync(InstitutionFilterRequestDTO filterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Institution.Institution>().Where(q => q.Status == Core.Enumarations.StatusType.Active);

            if (!string.IsNullOrEmpty(filterRequestDTO.Code))
                query = query.Where(q => q.Code == filterRequestDTO.Code);

            if (!string.IsNullOrEmpty(filterRequestDTO.EmailAddress))
                query = query.Where(q => q.EmailAddress == filterRequestDTO.EmailAddress);

            if (!string.IsNullOrEmpty(filterRequestDTO.Name))
                query = query.Where(q => q.Name == filterRequestDTO.Name);

            if (!string.IsNullOrEmpty(filterRequestDTO.PhoneNumber))
                query = query.Where(q => q.PhoneNumber == filterRequestDTO.PhoneNumber);


            query = query.Skip(filterRequestDTO.Start * filterRequestDTO.Length).Take(filterRequestDTO.Length);

            var institutionList = await query.ToListAsync();
            return institutionList ?? new List<Domain.Model.Institution.Institution>();
        }

        public async Task<Guid> UpdateAsync(Guid Id, InstitutionRequestDTO requestDTO)
        {
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(Id);
            if (institution == null)
                return Guid.Empty;
            institution.Code = requestDTO.Code;
            institution.EmailAddress = requestDTO.EmailAddress;
            institution.PhoneNumber = requestDTO.PhoneNumber;
            institution.Name = requestDTO.Name;
            institution.Status = requestDTO.Status;
            _repository.Update(institution);
            await _repository.CommitAsync();
            return Id;
        }
        public async Task<Guid> CreateAsync(InstitutionRequestDTO requestDTO)
        {
            var institution = new Domain.Model.Institution.Institution
            {
                Name = requestDTO.Name,
                PhoneNumber = requestDTO.PhoneNumber,
                EmailAddress = requestDTO.EmailAddress,
                Code = requestDTO.Code
            };
            _repository.Add(institution);
            await _repository.CommitAsync();
            return institution.Id;
        }
    }
}
