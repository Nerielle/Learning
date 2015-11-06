using System;
using System.Linq;
using System.Web.Mvc;
using Dal;
using Dal.Mapping;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository repository = new Repository();

        public ActionResult Index()
        {
            var articles = repository.GetArticles().OrderByDescending(x=>x.Date).ToList();
            return View(articles);
        }

        public ActionResult AddNewArticle()
        {
            var article = new Article() { Id = Guid.Empty};
           
            return View(article);

        }

        [HttpPost]
        public void DeleteComment(Comment comment)
        {
            using (var repository1 = new Repository())
            {
                repository1.Delete(comment);
            }
        }

        [HttpPost]
        public void SaveArticle(Article article)
        {
            using (var repository1 = new Repository())
            {
                article.Date = DateTime.Now;
                repository1.Save(article);
            }
        }

        [HttpPost]
        public void SaveComment(Comment comment)
        {
            using (var repository1 = new Repository())
            {
                var article = repository1.GetById<Article>(comment.ArticleId);
                comment.Date = DateTime.Now;
                article.Comments.Add(comment);
                repository1.Save(article);
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}