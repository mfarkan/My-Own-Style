using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Service.Model.Expenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HasTextile.API.Controllers
{
    public class ExpenseController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IExpensesService _expensesService;
        public ExpenseController(IMapper mapper, IExpensesService expensesService)
        {
            _mapper = mapper;
            _expensesService = expensesService;
        }
    }
}
