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
        Task<List<Domain.Model.Income.Expenses>> GetExpenses(int page, int pageSize);
        Task<Guid> CreateNewExpense(ExpenseRequestDTO requestDTO);
        Task<List<Domain.Model.Income.Expenses>> GetExpensesWithFilter(ExpenseFilterRequestDTO filterRequestDTO);
        Task PassivateExpense(Guid Id);
        Task<Guid> UpdateExpense(Guid Id, ExpenseRequestDTO requestDTO);
        Task<Domain.Model.Income.Expenses> GetExpense(Guid Id);

        Task CreateSector(string sectorDescription);
        Task UpdateSector(Guid Id, string sectorDescription);
        Task DeleteSector(Guid Id);
        Task<Domain.Model.Sector.Sector> GetSectorAsync(Guid Id);

    }
}
