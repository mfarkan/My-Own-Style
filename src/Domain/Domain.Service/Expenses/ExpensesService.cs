using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Model.Income;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Expenses.Model;
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

        public async Task<Guid> CreateNewExpense(ExpenseRequestDTO requestDTO)
        {
            var customer = await _repository.Query<Domain.Model.Customer.Customer>().Where(q => q.Id == requestDTO.CustomerId).FirstOrDefaultAsync();
            if (customer != null)
            {
                var newInstance = new Domain.Model.Income.Expenses
                {
                    Amount = requestDTO.Amount,
                    CurrencyType = requestDTO.CurrencyType,
                    Description = requestDTO.Description,
                    DocumentNumber = requestDTO.DocumentNumber,
                    Expiry = requestDTO.Expiry,
                    ExpiryDate = requestDTO.ExpiryDate,
                    Type = requestDTO.Type,
                    Customer = customer,
                };
                _repository.Add(newInstance);
                await _repository.CommitAsync();
                return newInstance.Id;
            }
            return Guid.Empty;
        }

        public async Task<Domain.Model.Income.Expenses> GetExpense(Guid Id)
        {
            var result = await _repository.GetByIdAsync<Domain.Model.Income.Expenses>(Id);
            return result;
        }

        public async Task<List<Domain.Model.Income.Expenses>> GetExpenses(int page, int pageSize)
        {
            var skipSize = pageSize * (page - 1);
            var incomeList = await _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>()
                .Skip(skipSize).Take(pageSize).ToListAsync();
            return incomeList ?? new List<Domain.Model.Income.Expenses>();
        }
        public Task GetExpensesWithFilter(ExpenseFilterRequestDTO filterRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task PassivateExpense(Guid Id)
        {
            var expense = await _repository.GetByIdAsync<Domain.Model.Income.Expenses>(Id);
            if (expense == null)
                return;
            expense.Delete();
            _repository.Update(expense);
            await _repository.CommitAsync();
        }

        public async Task<Guid> UpdateExpense(Guid Id, ExpenseRequestDTO requestDTO)
        {
            var expense = await _repository.GetByIdAsync<Domain.Model.Income.Expenses>(Id);
            var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(Id);
            if (expense == null || customer == null)
                return Guid.Empty;

            expense.Amount = requestDTO.Amount;
            expense.CurrencyType = requestDTO.CurrencyType;
            expense.Description = requestDTO.Description;
            expense.DocumentNumber = requestDTO.DocumentNumber;
            expense.Expiry = requestDTO.Expiry;
            expense.ExpiryDate = requestDTO.ExpiryDate;
            expense.Type = requestDTO.Type;
            expense.Customer = customer;

            _repository.Update(expense);
            await _repository.CommitAsync();
            return expense.Id;
        }
    }
}
