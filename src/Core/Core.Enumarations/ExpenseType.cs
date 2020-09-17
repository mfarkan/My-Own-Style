using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum ExpenseType
    {
        [Display(Name = "Gider")]
        OutGoing = 1,
        [Display(Name = "Gelir")]
        InCome = 2
    }
}
