using Core.Enumarations;
using System;

namespace HaxTextile.WebAppCore.Models.Customer
{
    public class CreateOrUpdateCustomerViewModel
    {
        public string MethodType { get; set; }
        public Guid Id { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerTelephoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDescription { get; set; }
        public string CustomerAddress { get; set; }
        public CustomerType CustomerCompanyType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
