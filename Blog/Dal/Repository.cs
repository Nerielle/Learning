using System;
using System.Collections.Generic;
using System.Linq;
using Dal.Mapping;
using NHibernate.Linq;

namespace Dal
{
    public class Repository : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public Repository()
        {
            unitOfWork = new UnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void Delete(DomainObject domainObject)
        {
            unitOfWork.Delete(domainObject);
        }

        public IList<Article> GetArticles()
        {
            return unitOfWork.Query<Article>().ToList();
        }

        public IList<Article> GetAllArticlesWithComments()
        {
            return unitOfWork.Query<Article>()
                .Fetch(x => x.Comments)
                .ToList();
        }

        public Article GetArticleWithCommentsById(Guid articleId)
        {
            return unitOfWork.Query<Article>().Fetch(x => x.Comments).FirstOrDefault(x => x.Id == articleId);
        }

        public T GetById<T>(Guid id) where T : DomainObject
        {
            return unitOfWork.GetById<T>(id);
        }

        public void Save(DomainObject domainObject)
        {
            unitOfWork.Save(domainObject);
            unitOfWork.Commit();
        }
    }
}