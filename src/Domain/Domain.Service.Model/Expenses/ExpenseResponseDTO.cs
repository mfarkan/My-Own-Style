using Core.Enumarations;
using Domain.Service.Model.Customer;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Expenses
{
    public class ExpenseResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public Guid? BankAccountId { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// Expense Type
        /// </summary>
        public ExpenseType Type { get; set; }
        /// <summary>
        /// Expense Cost
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Expense Currency Type
        /// </summary>
        public CurrencyType CurrencyType { get; set; }
        /// <summary>
        /// Currency Description
        /// </summary>
        public string CurrencyDescription { get; set; }
        /// <summary>
        /// Expense Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Expense Document No
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// Expense Expiry
        /// </summary>
        public int? Expiry { get; set; }
        /// <summary>
        /// Expense Expiry Date
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
