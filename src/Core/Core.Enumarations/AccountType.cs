using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum AccountType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Vadeli Hesap")]
        Vadeli = 1,
        [Display(Name = "Vadesiz Hesap")]
        Vadesiz = 2,
        [Display(Name = "KMH")]
        Overdraft = 3,
        [Display(Name = "Kredi Kartı")]
        CreditCard = 4
    }
}
