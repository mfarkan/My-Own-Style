using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Institution
{
    // we should add Institution information about every related data with institution.
    public class InstitutionResponseDTO : BaseResponseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
