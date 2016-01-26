using NHibernate.Mapping.ByCode;

namespace Dal.Mapping
{
    public class GroupMap : DomainObjectMap<Group>
    {
        public GroupMap()
        {
            Table("dbo.[Group]");
            Cache(x => x.Usage(CacheUsage.ReadOnly));
            Property(x => x.Name);
            Bag(x => x.Registrations,
                colmap => colmap.Key(x => x.Column("GroupId")),
                map=>map.OneToMany());
        }
    }
}