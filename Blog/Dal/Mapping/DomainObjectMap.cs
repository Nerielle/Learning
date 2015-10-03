using System;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dal.Mapping
{
    public class DomainObjectMap<T>:ClassMapping<T> where T:DomainObject
    {
        public DomainObjectMap()
        {
            Id(x=>x.Id, map =>
            {
                map.Column("Id");
                map.Type(NHibernateUtil.Guid);
                map.Generator(Generators.GuidComb);
                map.UnsavedValue(Guid.Empty);
            });
            Property(x=>x.Date, map=>map.Type(NHibernateUtil.DateTime2));
        }
    }
}