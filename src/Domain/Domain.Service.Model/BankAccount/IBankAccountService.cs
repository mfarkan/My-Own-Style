using Domain.Service.Model.BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.BankAccount
{
    public interface IBankAccountService
    {
        Task CreateNewBankAccountAsync(BankAccountRequestDTO requestDTO);
        Task UpdateBankAccountAsync(Guid Id, BankAccountRequestDTO requestDTO);
        Task PassivateBankAccountAsync(Guid Id);
        Task GetBankAccountsWithFilterAsync(BankAccountFilterRequestDTO accountFilterRequestDTO);
        Task GetBankAccountAsync(Guid Id);

    }
}
