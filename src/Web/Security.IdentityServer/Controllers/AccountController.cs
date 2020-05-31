using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.IdentityServer.Models;

namespace Security.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = externalLogins,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Redirect(returnUrl);
                }
                else if (result.IsLockedOut)
                {
                    return RedirectToAction("LockedOut", "Account");
                }
            }
            return View();
        }
        public IActionResult LockedOut()
        {
            return View();
        }
    }
}
