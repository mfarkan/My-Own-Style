using Core.Enumarations;
using System;
using System.ComponentModel.DataAnnotations;

namespace HaxTextile.WebAppCore.Models.Customer
{
    public class CreateOrUpdateCustomerViewModel
    {
        public string MethodType { get; set; }
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Mail adresi maksimum 50 karakter olabilir.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Mail Adresinizi kontrol ediniz.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string CustomerEmailAddress { get; set; }
        [StringLength(20, ErrorMessage = "Telefon numarası maksimum 50 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string CustomerTelephoneNumber { get; set; }
        [StringLength(100, ErrorMessage = "Müşteri ismi maksimum 100 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string CustomerName { get; set; }
        [StringLength(150, ErrorMessage = "Müşteri açıklama alanı maksimum 150 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string CustomerDescription { get; set; }
        [StringLength(150, ErrorMessage = "Müşteri adres alanı maksimum 150 karakter olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public string CustomerAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan zorunludur.")]
        public CustomerType CustomerCompanyType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
