using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.BankAccount
{
    public class BankAccountResponseDTO : BaseResponseDTO
    {
        public string BankAccountName { get; set; }
        public string BankAccountDescription { get; set; }
        public BankType BankType { get; set; }
        public decimal TotalBalance { get; set; }
        public string AccountIBAN { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountNo { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public decimal UsableBalance { get; set; }
        public decimal BlockedBalance { get; set; }
    }
}
