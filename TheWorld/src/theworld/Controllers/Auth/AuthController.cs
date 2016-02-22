﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using theworld.Models;
using theworld.ViewModels;

namespace theworld.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Trips", "App");
                }

                return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
                if (ModelState.IsValid)
                {
                    var signInResult = await signInManager.PasswordSignInAsync(vm.UserName,
                        vm.Password,
                        true,
                        false);
                    if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Trips", "App");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                }
                    else
                    {
                        ModelState.AddModelError("", "User name or password incorrect");
                    }
                }

                return View();
            
        }
    }
}