using System;
using Dal.Mapping;

namespace Dal
{
    public class Comment:DomainObject
    {
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Article Article { get; set; }
    }
}