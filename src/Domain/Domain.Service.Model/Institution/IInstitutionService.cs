using Domain.Model.Institution;
using Domain.Service.Model.Institution.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.Institution
{
    public interface IInstitutionService
    {
        Task<Guid> CreateAsync(InstitutionRequestDTO requestDTO);
        Task<Guid> UpdateAsync(Guid Id, InstitutionRequestDTO requestDTO);
        Task DeleteAsync(Guid Id);
        Task<List<Domain.Model.Institution.Institution>> GetAllInstitutionsAsync();
        Task<Domain.Model.Institution.Institution> GetInstitutionAsync(Guid Id);
        Task<List<Domain.Model.Institution.Institution>> GetInstitutionsWithFilterAsync(InstitutionFilterRequestDTO filterRequestDTO);
    }
}
