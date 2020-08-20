using Core.Enumarations;
using Domain.Service.Model.Expenses.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.Expenses
{
    public interface IExpensesService
    {
        Task GetExpenses(int page, int pageSize);
        Task<Guid> CreateNewExpense(ExpenseRequestDTO requestDTO);
        Task GetExpensesWithFilter(ExpenseFilterRequestDTO filterRequestDTO);
        Task PassivateExpense(Guid Id);
        Task<Guid> UpdateExpense(Guid Id, ExpenseRequestDTO requestDTO);
        Task<Domain.Model.Income.Expenses> GetExpense(Guid Id);
    }
}
