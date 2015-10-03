using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class DomainObjectMap:ClassMapping<DomainObject>
    {
        public DomainObjectMap()
        {
            Id(x=>x.Id);
        }
    }
}