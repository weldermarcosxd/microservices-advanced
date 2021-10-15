using System;

namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        public long Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? LastUpdatedDate  { get; set; }
    }
}
