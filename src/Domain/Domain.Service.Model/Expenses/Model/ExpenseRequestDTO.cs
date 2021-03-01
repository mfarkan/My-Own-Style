using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Expenses.Model
{
    public class ExpenseRequestDTO
    {
        /// <summary>
        /// Müşteri Id
        /// </summary>
        public Guid BankAccountId { get; set; }
        /// <summary>
        /// Gelir mi Gider mi?
        /// </summary>
        public ExpenseType ExpenseType { get; set; }
        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Para Birimi
        /// </summary>
        public CurrencyType CurrencyType { get; set; }
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Belge Numarası
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// Vade gün
        /// </summary>
        public int? Expiry { get; set; }
        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        public Guid InstitutionId { get; set; }
    }
}
