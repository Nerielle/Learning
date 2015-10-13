using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Dal
{
    public class SessionFactory : IDisposable
    {
        private static readonly ISessionFactory sessionFactory;

        static SessionFactory()
        {
            var connectionString = @"Data Source=.\sqlexpress;Initial Catalog=BlogDatabase;Integrated Security=True";

            var configuration = new Configuration();
            configuration.DataBaseIntegration(
                x =>
                {
                    x.ConnectionString = connectionString;
                    x.Driver<SqlClientDriver>();
                    x.Dialect<MsSql2012Dialect>();
                });
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            mapper.BeforeMapBag += (modelInspector, member1, propertyCustomizer) =>
            {
                propertyCustomizer.Inverse(true);
                propertyCustomizer.Cascade(Cascade.All | Cascade.DeleteOrphans);
            };
            mapper.BeforeMapManyToOne +=
                (modelInspector, member1, propertyCustomizer) => { propertyCustomizer.NotNullable(true); };
            mapper.BeforeMapProperty += (inspector, member, customizer) => customizer.NotNullable(true);
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(mapping);
            sessionFactory = configuration.BuildSessionFactory();
        }

        public void Dispose()
        {
            sessionFactory.Dispose();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}