using Core.Enumarations;
using Domain.Model.Account;
using System;

namespace Domain.Model.Income
{
    public class Expenses : EntityBase
    {
        public virtual BankAccount BankAccount { get; set; }
        public virtual ExpenseType Type { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual string Description { get; set; }
        public virtual string DocumentNumber { get; set; }
        public virtual int? Expiry { get; set; }
        public virtual Institution.Institution Institution { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public bool IsIncome() => Type == ExpenseType.InCome;
        public bool IsOutCome() => Type == ExpenseType.OutGoing;
    }
}
