using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Expenses.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Service.Expenses
{
    public class ExpensesService : IExpensesService
    {
        private readonly IBusinessRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExpensesService(IBusinessRepository repository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<Guid> CreateNewExpense(ExpenseRequestDTO requestDTO)
        {
            var bankAccount = await _repository.GetByIdAsync<Domain.Model.Account.BankAccount>(requestDTO.BankAccountId);
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId);
            var sector = await _repository.GetByIdAsync<Domain.Model.Sector.Sector>(requestDTO.SectorId);

            if (bankAccount == null || institution == null || sector == null)
                return Guid.Empty;

            var newInstance = new Domain.Model.Income.Expenses
            {
                Amount = requestDTO.Amount,
                CurrencyType = requestDTO.CurrencyType,
                Description = requestDTO.Description,
                DocumentNumber = requestDTO.DocumentNumber,
                Expiry = requestDTO.Expiry,
                ExpiryDate = requestDTO.ExpiryDate,
                Type = requestDTO.ExpenseType,
                BankAccount = bankAccount,
                Institution = institution,
                Sector = sector
            };
            _repository.Add(newInstance);
            await _repository.CommitAsync();
            return newInstance.Id;
        }
        public async Task<Domain.Model.Sector.Sector> GetSectorAsync(Guid Id)
        {
            var currentSector = await _repository.QueryWithoutTracking<Domain.Model.Sector.Sector>()
                .Where(q => q.Id == Id && q.Status == StatusType.Active)
                .FirstOrDefaultAsync();
            return currentSector;
        }
        public async Task CreateSector(string sectorDescription)
        {
            var newInstance = new Domain.Model.Sector.Sector
            {
                SectorDescription = sectorDescription
            };
            _repository.Add(newInstance);
            await _repository.CommitAsync();
        }

        public async Task DeleteSector(Guid Id)
        {//TODO! sector'u delete edince bağlı olan tüm income'ları diğer sector kısmına atmak gerekiyor. Diğer isimli sector pasif edilemez.
            await _repository.PassivateEntityAsync<Domain.Model.Sector.Sector>(Id);
        }

        public async Task<Domain.Model.Income.Expenses> GetExpense(Guid Id)
        {
            var result = await _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>()
                .Include(q => q.BankAccount)
                .Include(q => q.Institution)
                .FirstOrDefaultAsync(q => q.Id == Id && q.Status == StatusType.Active);
            return result;
        }
        public async Task<List<Domain.Model.Income.Expenses>> GetExpenses(int page, int pageSize)
        {
            var skipSize = pageSize * (page - 1);
            var incomeList = await _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>()
                .Skip(skipSize).Take(pageSize).ToListAsync();
            return incomeList ?? new List<Domain.Model.Income.Expenses>();
        }
        public async Task<List<Domain.Model.Income.Expenses>> GetExpensesWithFilter(ExpenseFilterRequestDTO filterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>().Where(q => q.Status == StatusType.Active);

            if (filterRequestDTO.BankAccountId.HasValue)
                query = query.Where(q => q.BankAccount.Id == filterRequestDTO.BankAccountId);
            if (!string.IsNullOrEmpty(filterRequestDTO.Description))
                query = query.Where(q => q.Description.Contains(filterRequestDTO.Description));
            if (filterRequestDTO.ExpenseType.HasValue)
                query = query.Where(q => q.Type == filterRequestDTO.ExpenseType.Value);
            if (!string.IsNullOrEmpty(filterRequestDTO.DocumentNumber))
                query = query.Where(q => q.DocumentNumber == filterRequestDTO.DocumentNumber);
            if (filterRequestDTO.Expiry.HasValue)
                query = query.Where(q => q.Expiry == filterRequestDTO.Expiry.Value);
            if (filterRequestDTO.ExpiryDate.HasValue)
                query = query.Where(q => q.ExpiryDate == filterRequestDTO.ExpiryDate.Value);
            query = query.Skip(filterRequestDTO.Start * filterRequestDTO.Length).Take(filterRequestDTO.Length);
            var expenseList = await query.Include(q => q.BankAccount).ToListAsync();
            return expenseList;
        }

        public async Task PassivateExpense(Guid Id)
        {
            await _repository.PassivateEntityAsync<Domain.Model.Income.Expenses>(Id);
        }

        public async Task<Guid> UpdateExpense(Guid Id, ExpenseRequestDTO requestDTO)
        {
            var expense = await _repository.Query<Domain.Model.Income.Expenses>().Where(q => q.Id == Id && q.Status == StatusType.Active)
               .Include(q => q.BankAccount)
               .Include(q => q.Institution)
               .FirstOrDefaultAsync();

            var bankAccount = await _repository.GetByIdAsync<Domain.Model.Account.BankAccount>(requestDTO.BankAccountId);
            var sector = await _repository.GetByIdAsync<Domain.Model.Sector.Sector>(requestDTO.SectorId);
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId);
            if (expense == null || bankAccount == null || institution == null || sector == null)
                return Guid.Empty;

            expense.Amount = requestDTO.Amount;
            expense.Sector = sector;
            expense.CurrencyType = requestDTO.CurrencyType;
            expense.Description = requestDTO.Description;
            expense.DocumentNumber = requestDTO.DocumentNumber;
            expense.Expiry = requestDTO.Expiry;
            expense.ExpiryDate = requestDTO.ExpiryDate;
            expense.Type = requestDTO.ExpenseType;
            expense.BankAccount = bankAccount;

            _repository.Update(expense);
            await _repository.CommitAsync();
            return expense.Id;
        }

        public async Task UpdateSector(Guid Id, string sectorDescription)
        {
            var currentSector = await _repository.GetByIdAsync<Domain.Model.Sector.Sector>(Id);
            currentSector.SectorDescription = sectorDescription;
            _repository.Update(currentSector);
            await _repository.CommitAsync();

        }
    }
}
