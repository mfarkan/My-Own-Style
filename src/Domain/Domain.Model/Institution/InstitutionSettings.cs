using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Institution
{
    public class InstitutionSettings : EntityBase
    {
        public virtual Institution Institution { get; set; }
    }
}
