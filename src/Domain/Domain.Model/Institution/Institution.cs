using Domain.Model.Customer;
using Domain.Model.Income;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Institution
{
    public class Institution : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual List<Expenses> ExpenseList { get; set; }
        public virtual List<ApplicationUser> UserList { get; set; }
    }
}
