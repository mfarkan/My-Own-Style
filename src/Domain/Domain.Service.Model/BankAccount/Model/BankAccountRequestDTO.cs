using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.BankAccount.Model
{
    public class BankAccountRequestDTO
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
        public Guid? InstitutionId { get; set; }
    }
}
