using System;

namespace Dal.Mapping
{
    public class DomainObject
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Date { get; set; }

    }
}