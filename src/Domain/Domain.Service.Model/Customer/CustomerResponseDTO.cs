using Core.Enumarations;
using Domain.Service.Model.Expenses;
using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;

namespace Domain.Service.Model.Customer
{
    public class CustomerResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Customer email address
        /// </summary>
        public string CustomerEmailAddress { get; set; }
        /// <summary>
        /// Customer phone number
        /// </summary>
        public string CustomerTelephoneNumber { get; set; }
        /// <summary>
        /// Customer name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Customer description
        /// </summary>
        public string CustomerDescription { get; set; }
        /// <summary>
        /// Customer address
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Customer Type ( 1: corporate , 2 : individual)
        /// </summary>
        public CustomerType CustomerCompanyType { get; set; }
        /// <summary>
        /// Customer's expense list.
        /// </summary>
        public List<ExpenseResponseDTO> Expenses { get; set; }
    }
}
