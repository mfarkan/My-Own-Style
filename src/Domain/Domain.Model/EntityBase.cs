using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class EntityBase
    {
        public StatusType Status { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
        public string CreatedByUserName { get; set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Status = StatusType.Active;
        }
    }
}
