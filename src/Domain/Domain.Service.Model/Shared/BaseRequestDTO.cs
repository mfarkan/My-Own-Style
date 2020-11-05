using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Model.Shared
{
    public abstract class BaseRequestDTO
    {
        public StatusType Status { get; set; }
    }
}
