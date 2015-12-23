using System;
using System.Collections.Generic;
using System.Web.Http;
using Dal;

namespace BlogWithAngular.Controllers
{
    public class ArticleController : ApiController
    {
        private readonly Repository repository = new Repository();
        // DELETE: api/Article/5
        public void Delete(int id)
        {
        }

        // GET: api/Article
        public IEnumerable<Article> Get()
        {
            return repository.GetArticles();
        }

        // GET: api/Article/5
        public Article Get(Guid id)
        {
            return repository.GetById<Article>(id);
        }

        // POST: api/Article
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Article/5
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}