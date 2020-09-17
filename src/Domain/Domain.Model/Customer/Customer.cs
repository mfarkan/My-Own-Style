using Core.Enumarations;
using Domain.Model.Income;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Customer
{
    public class Customer : EntityBase
    {
        public virtual string CustomerEmailAddress { get; set; }
        public virtual string CustomerTelephoneNumber { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerDescription { get; set; }
        public virtual string CustomerAddress { get; set; }
        public virtual CustomerType CustomerCompanyType { get; set; }
        public virtual List<Expenses> Expenses { get; set; }
    }
}
