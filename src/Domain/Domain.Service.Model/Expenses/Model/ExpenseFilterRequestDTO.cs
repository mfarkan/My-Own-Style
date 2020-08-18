using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Expenses.Model
{
    public class ExpenseFilterRequestDTO : BaseRequestDTO
    {
        public ExpenseType? ExpenseType { get; set; }
        public Guid? CustomerId { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public int? Expiry { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
