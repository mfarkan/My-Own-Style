using Domain.Service.Model.BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.BankAccount
{
    public interface IBankAccountService
    {
        Task<Guid> CreateNewBankAccountAsync(BankAccountRequestDTO requestDTO);
        Task<Guid> UpdateBankAccountAsync(Guid Id, BankAccountRequestDTO requestDTO);
        Task PassivateBankAccountAsync(Guid Id);
        Task<List<Domain.Model.Account.BankAccount>> GetBankAccountsWithFilterAsync(BankAccountFilterRequestDTO accountFilterRequestDTO);
        Task<Domain.Model.Account.BankAccount> GetBankAccountAsync(Guid Id);

    }
}
