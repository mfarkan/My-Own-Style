using Core.Enumarations;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Customer.Model
{
    /// <summary>
    /// Customer Filter Request DTO
    /// </summary>
    public class CustomerFilterRequestDTO : BaseFilterRequestDTO
    {
        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Customer address
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Customer email
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// customer phone number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }
        /// <summary>
        /// Customer Type
        /// </summary>
        public CustomerType? CustomerType { get; set; }

    }
}
