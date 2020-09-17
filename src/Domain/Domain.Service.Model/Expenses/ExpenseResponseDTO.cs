using Core.Enumarations;
using Domain.Service.Model.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Expenses
{
    public class ExpenseResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string CurrencyDescription { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public int? Expiry { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
