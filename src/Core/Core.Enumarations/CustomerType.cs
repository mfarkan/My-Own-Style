using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum CustomerType
    {
        /// <summary>
        /// Tüzel Müşteri
        /// </summary>
        [Display(Name = "Tüzel")]
        Corporate = 1,
        /// <summary>
        /// Şahız Müşteri
        /// </summary>
        [Display(Name = "Şahıs")]
        individual = 2,
    }
}
