﻿using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Expenses.Model
{
    public class ExpenseFilterRequestDTO : BaseFilterRequestDTO
    {
        /// <summary>
        /// Gelir mi(1) Gider mi(2) ?
        /// </summary>
        public ExpenseType? ExpenseType { get; set; }
        /// <summary>
        /// Banka Hesap Bilgisi
        /// </summary>
        public Guid? BankAccountId { get; set; }
        /// <summary>
        /// Açıklama Alanı
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Döküman Numarası
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// Vade Gün Sayısı
        /// </summary>
        public int? Expiry { get; set; }
        /// <summary>
        /// Vade Tarihi Sayısı
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
