using System;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.AspNet.Authorization;
using theworld;
using theworld.Models;
using theworld.Services;
using theworld.ViewModels;

namespace theworld.Controllers.Web
{
  public class AppController : Controller
  {
    private IMailService _mailService;
    private IWorldRepository _repository;

    public AppController(IMailService service, IWorldRepository repository)
    {
      _mailService = service;
      _repository = repository;
    }

    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Trips()
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
          ModelState.AddModelError("", "Could not send email, configuration problem.");
        }
        
        if (_mailService.SendMail(email,
          email,
          $"Contact Page from {model.Name} ({model.Email})",
          model.Message))
        {
          ModelState.Clear();

          ViewBag.Message = "Mail Sent. Thanks!";

        }
      }

      return View();
    }
  }
}
