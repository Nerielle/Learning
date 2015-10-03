using System;
using System.Linq;
using Dal.Mapping;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Persister.Entity;

namespace Dal
{
    public class UnitOfWork:IDisposable
    {
        private ISession session;
        private ITransaction transacion;
        private bool isCommited;
        public UnitOfWork()
        {
            session = SessionFactory.OpenSession();
            transacion = session.BeginTransaction();
        }

        public void Save(DomainObject domainObject)
        {
            session.SaveOrUpdate(domainObject);
        }
        public void Commit()
        {
            transacion.Commit();
            isCommited = true;
        }

        public void Dispose()
        {
            if (!isCommited)
            {
                transacion.Commit();
            }
            session.Dispose();
        }

        public DomainObject GetById<T>(Guid id) where T: DomainObject
        {
            return session.Get<T>(id);
        }

        public IQueryable<T> Query<T>()
        {
            return session.Query<T>();
        }
    }
}