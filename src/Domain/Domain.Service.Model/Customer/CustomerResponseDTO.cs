﻿using Core.Enumarations;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;

namespace Domain.Service.Model.Customer
{
    public class CustomerResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Müşteri Email
        /// </summary>
        public string CustomerEmailAddress { get; set; }
        /// <summary>
        /// Müşteri telefon bilgisi
        /// </summary>
        public string CustomerTelephoneNumber { get; set; }
        /// <summary>
        /// Müşteri Ad bilgisi
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Müşteri açıklaması
        /// </summary>
        public string CustomerDescription { get; set; }
        /// <summary>
        /// Müşteri adresi
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Müşteri tipi ( 1: tüzel , 2 : şirket)
        /// </summary>
        public CustomerType CustomerCompanyType { get; set; }
        /// <summary>
        /// Müşteriye ait Gelir/Gider bilgisi.
        /// </summary>
        public List<ExpenseResponseDTO> Expenses { get; set; }
    }
}
