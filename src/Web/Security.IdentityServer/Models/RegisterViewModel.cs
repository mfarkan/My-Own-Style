using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Security.IdentityServer.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "InvalidEmailError")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 12, MinimumLength = 6, ErrorMessage = "StringLengthError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PassWord", ErrorMessage = "ComparePasswordError")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
