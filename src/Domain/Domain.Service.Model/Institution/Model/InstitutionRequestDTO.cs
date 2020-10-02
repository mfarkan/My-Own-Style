using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Institution.Model
{
    public class InstitutionRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
