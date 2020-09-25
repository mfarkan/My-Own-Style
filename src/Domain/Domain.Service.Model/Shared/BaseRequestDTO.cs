using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Shared
{
    public abstract class BaseRequestDTO
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public Guid InstitutionId { get; set; }
    }
}
