using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.Expenses
{
    public interface IExpensesService
    {
        Task GetExpenses(int page, int pageSize);
        Task CreateNewExpense();
        Task GetExpensesWithFilter(DateTime? ExpiryDate, int? Expiry, string DocumentNumber, 
            string Description, Guid? CustomerId, ExpenseType? expenseType, int page = 1, int pagesize = 10);
        Task PassivateExpense();
        Task UpdateExpense();
        Task<Domain.Model.Income.Expenses> GetExpense(Guid Id);
    }
}
