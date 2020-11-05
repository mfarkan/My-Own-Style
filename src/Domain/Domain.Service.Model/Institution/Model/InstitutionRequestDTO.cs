using Domain.Service.Model.Shared;

namespace Domain.Service.Model.Institution.Model
{
    public class InstitutionRequestDTO : BaseRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
