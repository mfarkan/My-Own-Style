using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Customer.Model
{
    /// <summary>
    /// Müşteriyi filtrelemek için kullanılan class.
    /// </summary>
    public class CustomerFilterRequestDTO : BaseRequestDTO
    {
        /// <summary>
        /// Müşteri Ad Bilgisi
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Müşteri Adres Bilgisi
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Müşteri email bilgisi
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// Müşteri Telefon Numarası
        /// </summary>
        public string CustomerPhoneNumber { get; set; }
        /// <summary>
        /// Müşteri Tipi (1:tüzel , 2:şahıs)
        /// </summary>
        public CustomerType? CustomerType { get; set; }

    }
}
