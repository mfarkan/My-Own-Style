using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.BankAccount.Model
{
    public class BankAccountFilterRequestDTO : BaseFilterRequestDTO
    {
        public string BankAccountName { get; set; }
        public BankType? BankType { get; set; }
        public string AccountIBAN { get; set; }
        public AccountType? AccountType { get; set; }
        public string AccountNo { get; set; }
        public CurrencyType? CurrencyType { get; set; }
    }
}
