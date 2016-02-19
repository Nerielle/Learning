using System;
using System.Collections.Generic;
using System.Linq;
using Dal;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using Xunit;

namespace UnitTests
{
    public class CacheTests
    {
        [Fact]
        public void TestCache()
        {
            NHibernateProfiler.Initialize();
            var groupNames = new[] {"a1", "b1", "c1"};
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.CreateSQLQuery("delete from dbo.Registrations;delete from dbo.[Group]")
                    .ExecuteUpdate();
                session.CreateSQLQuery("delete from dbo.[User]");
                groupNames.ForEach(x =>
                {
                    var group = new Group {Id = Guid.NewGuid(), Name = x, Date = DateTime.UtcNow};
                    session.Save(group);
                });
                tx.Commit();
            }
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var user = new User {Name = "User1", Id = Guid.NewGuid(), Date = DateTime.UtcNow};
                session.Save(user);
                tx.Commit();
            }
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var user = session.Query<User>().First();
                var groups = GetGroups(session);
                var group = groups.First();
                var registration = new GroupRegistration
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Group = group,
                    Date = DateTime.UtcNow
                };
                user.Registrations.Add(registration);
                group.Registrations.Add(registration);
                session.Save(registration);
                session.Flush();
                tx.Commit();
            }
            using (var session = SessionFactory.OpenSession())
            {
                var g = GetGroups(session);
            }
            using (var session = SessionFactory.OpenSession())
            {
                var g = GetGroups(session);
            }
        }

        private static List<Group> GetGroups(ISession session)
        {
            return session.Query<Group>()
                .Cacheable()
                .CacheMode(CacheMode.Normal)
                .ToList();
        }
    }
}