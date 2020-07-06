using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaxTextile.WebAppCore.Models.Customer
{
    public class CustomerSearchRequestDTO : BaseRequestDTO
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public CustomerType? CustomerType { get; set; }
    }
}
