using System;

namespace Domain.Service.Model.Shared
{
    public abstract class BaseFilterRequestDTO
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public Guid? InstitutionId { get; set; }
    }
}
