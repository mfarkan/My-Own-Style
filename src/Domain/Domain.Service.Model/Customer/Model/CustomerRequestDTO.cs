using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Customer.Model
{
    public class CustomerRequestDTO
    {
        /// <summary>
        /// Müşteri Adı
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Müşteri Adres Bilgisi
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Müşteri mail adresi
        /// </summary>
        public string CustomerEmailAddress { get; set; }
        /// <summary>
        /// Müşteri telefon numarası
        /// </summary>
        public string CustomerTelephoneNumber { get; set; }
        /// <summary>
        /// Müşteri açıklaması
        /// </summary>
        public string CustomerDescription { get; set; }
        /// <summary>
        /// Müşteri Tipi
        /// </summary>
        public CustomerType CustomerType { get; set; }
    }
}
