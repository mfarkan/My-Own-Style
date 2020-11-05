﻿using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Model.Income;
using Domain.Model.Institution;
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
            var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(requestDTO.CustomerId);
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId);

            if (customer == null || institution == null)
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
                Customer = customer,
                Institution = institution,
            };
            _repository.Add(newInstance);
            await _repository.CommitAsync();
            return newInstance.Id;
        }
        public async Task<Domain.Model.Income.Expenses> GetExpense(Guid Id)
        {
            var result = await _repository.Query<Domain.Model.Income.Expenses>().Where(q => q.Id == Id && q.Status == StatusType.Active)
                .Include(q => q.Customer)
                .Include(q => q.Institution)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<Domain.Model.Income.Expenses>> GetExpenses(int page, int pageSize)
        {
            var skipSize = pageSize * (page - 1);
            var incomeList = await _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>().Include(q => q.Customer)
                .Skip(skipSize).Take(pageSize).ToListAsync();
            return incomeList ?? new List<Domain.Model.Income.Expenses>();
        }
        public async Task<List<Domain.Model.Income.Expenses>> GetExpensesWithFilter(ExpenseFilterRequestDTO filterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>().Where(q => q.Status == StatusType.Active);

            if (filterRequestDTO.CustomerId.HasValue)
                query = query.Where(q => q.Customer.Id == filterRequestDTO.CustomerId);
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
            var expenseList = await query.Include(q => q.Customer).ToListAsync();
            return expenseList;
        }

        public async Task PassivateExpense(Guid Id)
        {
            await _repository.PassivateEntityAsync<Domain.Model.Income.Expenses>(Id);
            await Task.CompletedTask;
        }

        public async Task<Guid> UpdateExpense(Guid Id, ExpenseRequestDTO requestDTO)
        {
            var expense = await _repository.Query<Domain.Model.Income.Expenses>().Where(q => q.Id == Id && q.Status == StatusType.Active)
               .Include(q => q.Customer)
               .Include(q => q.Institution)
               .FirstOrDefaultAsync();

            var customer = await _repository.GetByIdAsync<Domain.Model.Customer.Customer>(requestDTO.CustomerId);
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId);
            if (expense == null || customer == null || institution == null)
                return Guid.Empty;

            expense.Amount = requestDTO.Amount;
            expense.CurrencyType = requestDTO.CurrencyType;
            expense.Description = requestDTO.Description;
            expense.DocumentNumber = requestDTO.DocumentNumber;
            expense.Expiry = requestDTO.Expiry;
            expense.ExpiryDate = requestDTO.ExpiryDate;
            expense.Type = requestDTO.ExpenseType;
            expense.Customer = customer;

            _repository.Update(expense);
            await _repository.CommitAsync();
            return expense.Id;
        }
    }
}
