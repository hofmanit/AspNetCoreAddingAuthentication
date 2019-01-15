using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WishList.Models;
using WishList.Models.AccountViewModels;    

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model )
        {
            if (!ModelState.IsValid)
               return View(model);
            var result = _userManager.CreateAsync(new ApplicationUser() { Email = model.Email, UserName = model.Email }, model.Password);

            if (!result.Result.Succeeded)
            {
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("Password", "Not valid login");
                    return View(model);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

