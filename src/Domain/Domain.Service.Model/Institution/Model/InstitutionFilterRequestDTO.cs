using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Institution.Model
{
    public class InstitutionFilterRequestDTO : BaseFilterRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
