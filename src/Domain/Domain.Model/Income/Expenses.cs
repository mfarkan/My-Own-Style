using Core.Enumarations;
using System;

namespace Domain.Model.Income
{
    public class Expenses : EntityBase
    {
        public virtual Customer.Customer Customer { get; set; }
        public virtual ExpenseType Type { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual string Description { get; set; }
        public virtual string DocumentNumber { get; set; }
        public virtual int? Expiry { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
    }
}
