using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Model.Income;
using Domain.Service.Model.Expenses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Expenses
{
    public class ExpensesService : IExpensesService
    {

        private readonly IBusinessRepository _repository;
        public ExpensesService(IBusinessRepository repository)
        {
            _repository = repository;
        }
        public Task CreateNewExpense()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Model.Income.Expenses> GetExpense(Guid Id)
        {
            var result = await _repository.Query<Domain.Model.Income.Expenses>().Where(q => q.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public Task GetExpenses(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task GetExpensesWithFilter(DateTime? ExpiryDate, int? Expiry, string DocumentNumber, string Description, Guid? CustomerId, ExpenseType? expenseType, int page = 1, int pagesize = 10)
        {
            throw new NotImplementedException();
        }

        public Task PassivateExpense()
        {
            throw new NotImplementedException();
        }

        public Task UpdateExpense()
        {
            throw new NotImplementedException();
        }
    }
}
