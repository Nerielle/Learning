using System;
using System.Collections.Generic;
using Dal.Mapping;

namespace Dal
{
    public class Article:DomainObject
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<Comment> Comments { get; set; } 
    }
}