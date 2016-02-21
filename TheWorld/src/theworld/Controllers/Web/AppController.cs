using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using theworld.Models;
using theworld.Services;
using theworld.ViewModels;

namespace theworld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private IWorldRepository repository;

        public AppController(IMailService service, IWorldRepository context)
        {
            mailService = service;
            repository = context;
        }

        public IActionResult Index()
        {
            var trips = repository.GetAllTrips();
            return View(trips);
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