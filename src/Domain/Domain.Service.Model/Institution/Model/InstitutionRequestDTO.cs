using Domain.Service.Model.Shared;

namespace Domain.Service.Model.Institution.Model
{
    public class InstitutionRequestDTO : BaseRequestDTO
    {
        /// <summary>
        /// Institution Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Institution Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Institution Email
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Institution Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
