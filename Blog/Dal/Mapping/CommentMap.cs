using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class CommentMap : DomainObjectMap<Comment>
    {
        public CommentMap()
        {
            Table("Comment");
            Property(x => x.Content);
            ManyToOne(x => x.Article, map=>map.Column("ArticleId"));
        }
    }
}