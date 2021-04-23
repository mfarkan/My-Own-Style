using AutoMapper;
using Core.Enumarations;
using Domain.Model.Account;
using Domain.Model.Income;
using Domain.Service.Model.BankAccount;
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
                .ForMember(dest => dest.BankAccountId, src => src.MapFrom(map => map.BankAccount.Id))
                .ForMember(dest => dest.CurrencyDescription, src => src.MapFrom(map => map.CurrencyType.GetDisplayName()))
                .ForMember(dest => dest.BankAccountName, src => src.MapFrom(map => map.BankAccount.BankAccountName));

            CreateMap<BankAccount, BankAccountResponseDTO>();
        }
    }
}
