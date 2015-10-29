using System;
using System.Linq;
using System.Web.Mvc;
using Dal;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository repository = new Repository();

        public ActionResult Index()
        {
            var articles = repository.GetArticles();
            ViewBag.SelectedOption = articles.Any() ? articles.First().Id : Guid.Empty;
            return View(articles);
        }

        public ActionResult AddNewArticle()
        {
            var article = new Article() {Title = "New article", Id = Guid.Empty};
           
            return View(article);

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