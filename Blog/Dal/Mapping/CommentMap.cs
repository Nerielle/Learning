using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class CommentMap : DomainObjectMap<Comment>
    {
        public CommentMap()
        {
            Table("Comment");
            Property(x => x.Content);
            Property(x => x.Date);
            ManyToOne(x => x.Article);
        }
    }
}