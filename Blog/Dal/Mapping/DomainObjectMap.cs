using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class DomainObjectMap<T>:ClassMapping<T> where T:DomainObject
    {
        public DomainObjectMap()
        {
            Id(x=>x.Id);
        }
    }
}