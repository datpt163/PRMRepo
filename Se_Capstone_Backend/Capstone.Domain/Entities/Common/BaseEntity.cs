using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        public Guid? CreatedBy { get; set; }

        [MaxLength(100)]
        public Guid? UpdatedBy { get; set; }
    }
}
