using System;
using System.Collections.Generic;
using System.Linq;
using Dal.Mapping;

namespace Dal
{
    public class Repository:IDisposable
    {
        private readonly UnitOfWork unitOfWork;
        public Repository()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public IList<Article> GetArticles()
        {
            return unitOfWork.Query<Article>().ToList();
        }
        public T GetById<T>(Guid id) where T: DomainObject
        {
            return unitOfWork.GetById<T>(id);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void Save(DomainObject domainObject)
        {
            unitOfWork.Save(domainObject);
            unitOfWork.Commit();
        }
    }
}