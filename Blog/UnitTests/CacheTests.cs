using System;
using Dal;
using NHibernate.Util;
using Xunit;

namespace UnitTests
{
    public class CacheTests
    {
        [Fact]
        public void TestCache()
        {
            var groupNames = new[] {"a1", "b1", "c1"};
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.CreateSQLQuery("delete from dbo.Registrations;delete from dbo.[Group]").ExecuteUpdate();
                groupNames.ForEach(x =>
                {
                    var group = new Group {Id = Guid.NewGuid(), Name = x, Date = DateTime.UtcNow};
                    session.Save(group);
                });
                tx.Commit();
            }
        }
    }
}