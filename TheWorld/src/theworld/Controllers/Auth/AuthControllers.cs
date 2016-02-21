using Microsoft.AspNet.Mvc;

namespace theworld.Controllers
{
    public class AuthControllers: Controller
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }
    }
}