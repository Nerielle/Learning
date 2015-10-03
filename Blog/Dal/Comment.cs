using System;

namespace Dal
{
    public class Comment
    {
        public virtual Guid Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Article Article { get; set; }
    }
}