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

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}