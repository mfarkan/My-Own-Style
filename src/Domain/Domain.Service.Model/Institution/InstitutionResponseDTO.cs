using Domain.Service.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Institution
{
    // we should add Institution information about every related data with institution.
    public class InstitutionResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Institution Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Institution Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Institution Email
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Institution Phone
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
