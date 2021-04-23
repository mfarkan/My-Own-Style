using Domain.Model.Income;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Sector
{
    public class Sector : EntityBase
    {
        public virtual string SectorDescription { get; set; }
        public List<Expenses> Expenses { get; set; }
    }
}
