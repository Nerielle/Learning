using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class ArticleMap:ClassMapping<Article>
    {
        public ArticleMap()
        {
            Table("Article");
            Property(x=>x.Content);
            Property(x=>x.Date);
            Property(x=>x.Description, map=>map.NotNullable(false));
            Property(x=>x.Title);
            Bag(x=>x.Comments, map =>
            {
                map.Key(x=>x.Column("ArticleId"));
            });
        }
    }
}