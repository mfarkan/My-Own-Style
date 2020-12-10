using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum ExpenseType
    {
        /// <summary>
        /// Outcome
        /// </summary>
        [Display(Name = "Gider")]
        OutGoing = 1,
        /// <summary>
        /// Income
        /// </summary>
        [Display(Name = "Gelir")]
        InCome = 2
    }
}
