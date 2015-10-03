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

        public ActionResult Add()
        {
            var article = new Article() {Title = "New article", Id = Guid.Empty};
            var articles = repository.GetArticles();
            articles.Add(article);
            ViewBag.SelectedOption = Guid.Empty;
            return View("Index",articles);
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