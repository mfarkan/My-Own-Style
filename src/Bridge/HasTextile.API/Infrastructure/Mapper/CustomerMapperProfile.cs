using AutoMapper;
using Domain.Model.Customer;
using Domain.Service.Model.Customer;

namespace HasTextile.API.Infrastructure.Mapper
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<Customer, CustomerResponseDTO>();
        }
    }
}
