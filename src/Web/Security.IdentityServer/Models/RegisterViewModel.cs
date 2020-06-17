using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Security.IdentityServer.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "RequiredError")]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "RequiredError")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "InvalidEmailError")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredError")]
        [StringLength(maximumLength: 12, MinimumLength = 6, ErrorMessage = "StringLengthError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredError")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordCompare")]
        [Compare("Password", ErrorMessage = "ComparePasswordError")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
