using Core.Enumarations;
using Domain.DataLayer.Business;
using Domain.Service.Model.BankAccount;
using Domain.Service.Model.BankAccount.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Service.BankAccount
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBusinessRepository _repository;
        public BankAccountService(IBusinessRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateNewBankAccountAsync(BankAccountRequestDTO requestDTO)
        {
            var institution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId);
            if (institution == null)
                return Guid.Empty;

            var newInstance = new Domain.Model.Account.BankAccount
            {
                BankAccountName = requestDTO.BankAccountName,
                Institution = institution,
                AccountIBAN = requestDTO.AccountIBAN,
                AccountNo = requestDTO.AccountNo,
                AccountType = requestDTO.AccountType,
                BankAccountDescription = requestDTO.BankAccountDescription,
                BankType = requestDTO.BankType,
                BlockedBalance = requestDTO.BlockedBalance,
                CurrencyType = requestDTO.CurrencyType,
                TotalBalance = requestDTO.TotalBalance,
                UsableBalance = requestDTO.UsableBalance
            };
            _repository.Add(newInstance);
            await _repository.CommitAsync();
            return newInstance.Id;
        }

        public async Task<Domain.Model.Account.BankAccount> GetBankAccountAsync(Guid Id)
        {
            var result = await _repository.QueryWithoutTracking<Domain.Model.Account.BankAccount>()
                .FirstOrDefaultAsync(q => q.Id == Id && q.Status == Core.Enumarations.StatusType.Active);
            return result;
        }

        public Task<List<Domain.Model.Account.BankAccount>> GetBankAccountsWithFilterAsync(BankAccountFilterRequestDTO accountFilterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Income.Expenses>().Where(q => q.Status == StatusType.Active);

            throw new NotImplementedException();
        }

        public async Task PassivateBankAccountAsync(Guid Id)
        {
            await _repository.PassivateEntityAsync<Domain.Model.Account.BankAccount>(Id);
        }

        public async Task<Guid> UpdateBankAccountAsync(Guid Id, BankAccountRequestDTO requestDTO)
        {
            var currentBankAccount = await _repository.GetByIdAsync<Domain.Model.Account.BankAccount>(Id);
            if (currentBankAccount == null)
                return Guid.Empty;

            currentBankAccount.AccountIBAN = requestDTO.AccountIBAN;
            currentBankAccount.AccountNo = requestDTO.AccountNo;
            currentBankAccount.AccountType = requestDTO.AccountType;
            currentBankAccount.BankAccountDescription = requestDTO.BankAccountDescription;
            currentBankAccount.BankAccountName = requestDTO.BankAccountName;
            currentBankAccount.BankType = requestDTO.BankType;
            currentBankAccount.BlockedBalance = requestDTO.BlockedBalance;
            currentBankAccount.CurrencyType = requestDTO.CurrencyType;
            currentBankAccount.TotalBalance = requestDTO.TotalBalance;
            currentBankAccount.UsableBalance = requestDTO.UsableBalance;

            _repository.Update(currentBankAccount);
            await _repository.CommitAsync();
            return currentBankAccount.Id;
        }
    }
}
