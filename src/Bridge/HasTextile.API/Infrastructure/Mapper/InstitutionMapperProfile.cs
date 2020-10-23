using AutoMapper;
using Domain.Model.Institution;
using Domain.Service.Model.Institution;

namespace HasTextile.API.Infrastructure.Mapper
{
    public class InstitutionMapperProfile : Profile
    {
        public InstitutionMapperProfile()
        {
            CreateMap<Institution, InstitutionResponseDTO>();
        }
    }
}
