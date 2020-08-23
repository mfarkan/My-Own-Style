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
            CreateMap<Expenses, ExpenseResponseDTO>();
        }
    }
}
