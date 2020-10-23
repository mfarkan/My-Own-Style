using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Shared
{
    public abstract class BaseResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
