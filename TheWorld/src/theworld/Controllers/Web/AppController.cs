using Microsoft.AspNet.Mvc;

namespace theworld.Controllers.Web
{
    public class AppController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
