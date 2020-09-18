using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Institution
{
    public class Institution : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
