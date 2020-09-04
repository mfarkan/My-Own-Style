using Core.Enumarations;
using Domain.Service.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaxTextile.WebAppCore.Models.Expense
{
    public class CreateOrUpdateExpenseViewModel
    {
        public IEnumerable<CustomerResponseDTO> CustomerList { get; set; }
        public string MethodType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public ExpenseType ExpenseType { get; set; }

        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public Guid CustomerId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public decimal Amount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public CurrencyType CurrencyType { get; set; }
        [StringLength(100, ErrorMessage = "Açıklama alanı maksimum 100 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string Description { get; set; }
        [StringLength(48, ErrorMessage = "Döküman numarası alanı maksimum 48 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string DocumentNumber { get; set; }
        [Range(1, 10000, ErrorMessage = "Vade alanı 1 ila 10.000 arasında olmalıdır.")]
        public int? Expiry { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
