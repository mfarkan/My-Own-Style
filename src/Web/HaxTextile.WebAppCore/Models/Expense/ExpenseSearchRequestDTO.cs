using Core.Enumarations;
using Domain.Service.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaxTextile.WebAppCore.Models.Expense
{
    public class ExpenseSearchRequestDTO : BaseRequestDTO
    {
        public IEnumerable<CustomerResponseDTO> CustomerList { get; set; }
        public ExpenseType? ExpenseType { get; set; }
        public Guid? CustomerId { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public int? Expiry { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }
    }
}
