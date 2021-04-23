using Core.Enumarations;
using Core.Extensions;
using Domain.DataLayer.Business;
using Domain.Service.Model.BankAccount;
using Domain.Service.Model.BankAccount.Model;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BankAccountService(IBusinessRepository repository, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _httpContextAccessor = accessor;
        }
        private string CurrentInstitutionId
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.GetUserInstitutionId();// if it's null then user is admin.
            }
        }
        public async Task<Guid> CreateNewBankAccountAsync(BankAccountRequestDTO requestDTO)
        {

            Domain.Model.Institution.Institution currentInstitution;
            if (string.IsNullOrEmpty(CurrentInstitutionId))
            {//admin user so , ins id will come from request payload.
                currentInstitution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(requestDTO.InstitutionId.Value);
            }
            else
            {//casual user.
                currentInstitution = await _repository.GetByIdAsync<Domain.Model.Institution.Institution>(Guid.Parse(CurrentInstitutionId));
            }

            if (currentInstitution == null)
                return Guid.Empty;

            var newInstance = new Domain.Model.Account.BankAccount
            {
                BankAccountName = requestDTO.BankAccountName,
                Institution = currentInstitution,
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

        public async Task<List<Domain.Model.Account.BankAccount>> GetBankAccountsWithFilterAsync(BankAccountFilterRequestDTO accountFilterRequestDTO)
        {
            var query = _repository.QueryWithoutTracking<Domain.Model.Account.BankAccount>().Where(q => q.Status == StatusType.Active);

            if (!string.IsNullOrEmpty(accountFilterRequestDTO.AccountIBAN))
            {
                query = query.Where(q => q.AccountIBAN == accountFilterRequestDTO.AccountIBAN);
            }
            if (!string.IsNullOrEmpty(accountFilterRequestDTO.AccountNo))
            {
                query = query.Where(q => q.AccountNo == accountFilterRequestDTO.AccountNo);
            }
            if (accountFilterRequestDTO.AccountType.HasValue)
            {
                query = query.Where(q => q.AccountType == accountFilterRequestDTO.AccountType.Value);
            }
            if (!string.IsNullOrEmpty(accountFilterRequestDTO.BankAccountName))
            {
                query = query.Where(q => q.BankAccountName.Contains(accountFilterRequestDTO.BankAccountName));
            }
            if (accountFilterRequestDTO.BankType.HasValue)
            {
                query = query.Where(q => q.BankType == accountFilterRequestDTO.BankType.Value);
            }
            if (accountFilterRequestDTO.CurrencyType.HasValue)
            {
                query = query.Where(q => q.CurrencyType == accountFilterRequestDTO.CurrencyType.Value);
            }
            if (!string.IsNullOrEmpty(CurrentInstitutionId))
            { // casual user.
                query = query.Where(q => q.Institution.Id == Guid.Parse(CurrentInstitutionId));
            }
            if (accountFilterRequestDTO.InstitutionId.HasValue)
            {
                query = query.Where(q => q.Institution.Id == accountFilterRequestDTO.InstitutionId.Value);
            }
            query = query.Skip(accountFilterRequestDTO.Start * accountFilterRequestDTO.Length).Take(accountFilterRequestDTO.Length);
            var bankAccounts = await query.ToListAsync();
            return bankAccounts ?? new List<Domain.Model.Account.BankAccount>();
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
