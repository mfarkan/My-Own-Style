using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Enumarations
{
    public enum CustomerType
    {
        [Display(Name = "Tüzel")]
        // Tüzel kişi ve şirket
        Corporate = 1,
        // Şahıs şirketi veya şahıs.
        [Display(Name = "Şahıs")]
        individual = 2,
    }
}
