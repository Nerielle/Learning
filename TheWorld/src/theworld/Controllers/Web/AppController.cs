using Microsoft.AspNet.Mvc;
using theworld.Services;
using theworld.ViewModels;

namespace theworld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;

        public AppController(IMailService service)
        {
            mailService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email");
                }
                if (
                    mailService.SendMail(email,
                        "",
                        $"Contact Page from {model.Name} ({model.Email})",
                        model.Message))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Mail sent. Thanks";
                }
            }

            return View();
        }
    }
}