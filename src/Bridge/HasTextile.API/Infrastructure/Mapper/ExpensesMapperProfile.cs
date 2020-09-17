using AutoMapper;
using Domain.Model.Income;
using Domain.Service.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasTextile.API.Infrastructure.Mapper
{
    public class ExpensesMapperProfile : Profile
    {
        public ExpensesMapperProfile()
        {
            CreateMap<Expenses, ExpenseResponseDTO>()
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(map => map.Customer.Id))
                .ForMember(dest => dest.CurrencyDescription, src => src.MapFrom(map => map.CurrencyType.ToString()))
                .ForMember(dest => dest.CustomerName, src => src.MapFrom(map => map.Customer.CustomerName));
        }
    }
}
