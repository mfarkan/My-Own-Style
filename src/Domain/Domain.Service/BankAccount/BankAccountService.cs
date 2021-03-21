using Domain.DataLayer.Business;
using Domain.Service.Model.BankAccount;
using Domain.Service.Model.BankAccount.Model;
using Microsoft.EntityFrameworkCore;
using System;
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
        public Task CreateNewBankAccountAsync(BankAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task GetBankAccountAsync(Guid Id)
        {
            var bankAccount = _repository.QueryWithoutTracking<Domain.Model.Account.BankAccount>()
                .FirstOrDefaultAsync(q => q.Id == Id && );
            throw new NotImplementedException();
        }

        public Task GetBankAccountsWithFilterAsync(BankAccountFilterRequestDTO accountFilterRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task PassivateBankAccountAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBankAccountAsync(Guid Id, BankAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
