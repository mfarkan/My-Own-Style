using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Customer.Model
{
    public class CustomerRequestDTO
    {
        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Customer Address
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// Customer email address
        /// </summary>
        public string CustomerEmailAddress { get; set; }
        /// <summary>
        /// Customer Phone Number
        /// </summary>
        public string CustomerTelephoneNumber { get; set; }
        /// <summary>
        /// Customer Description
        /// </summary>
        public string CustomerDescription { get; set; }
        /// <summary>
        /// Customer Type
        /// </summary>
        public CustomerType CustomerCompanyType { get; set; }
        /// <summary>
        /// Customer's Institution
        /// </summary>
        public Guid InstitutionId { get; set; }
    }
}
