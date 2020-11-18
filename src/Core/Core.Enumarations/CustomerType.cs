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
        /// Corporate or Company
        /// </summary>
        [Display(Name = "Tüzel")]
        Corporate = 1,
        /// <summary>
        /// Individual company
        /// </summary>
        [Display(Name = "Şahıs")]
        Individual = 2,
    }
}
