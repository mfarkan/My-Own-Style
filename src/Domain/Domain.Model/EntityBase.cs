using Core.Enumarations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Model
{
    public abstract class EntityBase
    {
        public virtual StatusType Status { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedByIp { get; set; }
        public virtual string CreatedByUserName { get; set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Status = StatusType.Active;
        }
        public bool IsPassive() => Status == StatusType.Passive;
        public bool IsDeleted() => Status == StatusType.Deleted;
        public bool IsActive() => Status == StatusType.Active;
        public void Delete() => Status = StatusType.Deleted;
        public void Active() => Status = StatusType.Active;
    }
}
