using AspNet.Security.OpenIdConnect.Primitives;
using Domain.Model.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Security.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
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
            var openIdRequest = HttpContext.GetOpenIdConnectRequest();
            var paramList = openIdRequest.GetParameters();
            if (paramList != null && paramList.Any(m => m.Key == "culture"))
            {
                var culture = paramList.FirstOrDefault(m => m.Key == "culture");
                HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture.Value.ToString())),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
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
                else
                {
                    var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    model.ExternalLogins = externalLogins;
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> VerifyUser(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                ViewBag.Success = true;
                return View();
            }
            AddErrors(result);
            return View();
        }
        [NonAction]
        private string CallBackUrl(string controller, string action, Guid userId, string token, string scheme)
        {
            var callbackUrl = Url.Action(
                new Microsoft.AspNetCore.Mvc.Routing.UrlActionContext
                {
                    Action = action,
                    Controller = controller,
                    Protocol = scheme,
                    Values = new { userId, token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)) },
                });
            return callbackUrl;
        }
        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            RegisterViewModel model = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }
        public async Task<IActionResult> ExternalLoginCallBack()
        {
            var authenticationResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (authenticationResult?.Succeeded != true)
            {
                return RedirectToAction(nameof(Login));
            }

            var returnUrl = authenticationResult.Properties.Items["returnUrl"];
            var userName = authenticationResult.Principal.Claims.FirstOrDefault(x => x.Type == OpenIdConnectConstants.Claims.Name).Value.ToLower();


            var user = await _userManager.FindByLoginAsync(IISDefaults.AuthenticationScheme, userName);

            if (user != null)
            {
                var claimsPrincipal = await this._signInManager.CreateUserPrincipalAsync(user);
                foreach (var item in authenticationResult.Principal.Claims)
                {
                    if (item.Type != OpenIdConnectConstants.Claims.Name)
                    {
                        ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(item);
                    }
                }
                // if institutionId is null when redirect user to Admin pages.
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);
                return Redirect(returnUrl);
            }
            user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = "hastextile@hastekstil.com.tr",
                    UserName = userName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                string roleName = "HasTextileSystemAdmin";
                ApplicationRole systemRole = await _roleManager.FindByNameAsync(roleName);
                if (systemRole == null)
                {
                    systemRole = new ApplicationRole
                    {
                        Name = "HasTextileSystemAdmin",
                    };
                    await _roleManager.CreateAsync(systemRole);
                }
                var passWord = "fatih2626";
                var result = await _userManager.CreateAsync(user, passWord);
                await _userManager.AddToRoleAsync(user, systemRole.Name);
            }
            var loginInfo = new UserLoginInfo(IISDefaults.AuthenticationScheme, userName, IISDefaults.AuthenticationScheme);
            var identityAddLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
            if (identityAddLoginResult.Succeeded)
            {
                user = await this._userManager.FindByLoginAsync(IISDefaults.AuthenticationScheme, userName);
                var claimsPrincipal = await this._signInManager.CreateUserPrincipalAsync(user);
                foreach (var item in authenticationResult.Principal.Claims)
                {
                    if (item.Type != OpenIdConnectConstants.Claims.Name)
                    {
                        ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(item);
                    }
                }
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);
                // if institutionId is null when redirect user to Admin pages.
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string scheme, string returnUrl)
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("ExternalLoginCallBack"),
                Items =
                 {
                     { "returnUrl", returnUrl ?? string.Empty },
                     { "scheme", scheme }
                 }
            };
            if (scheme.Equals(IISDefaults.AuthenticationScheme))
            {
                var result = await HttpContext.AuthenticateAsync(IISDefaults.AuthenticationScheme);
                if (result != null && result.Principal != null && result.Principal is WindowsPrincipal)
                {
                    if (result.Principal.Identity.Name == "DESKTOP-HSUM9T4\\muratfatiharkan" || result.Principal.Identity.Name == "NU")
                    {
                        var principalIdentity = new ClaimsIdentity(scheme, OpenIdConnectConstants.Claims.Name, OpenIdConnectConstants.Claims.Role);
                        principalIdentity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Name, result.Principal.Identity.Name));
                        await HttpContext.SignInAsync(IdentityConstants.ExternalScheme, new ClaimsPrincipal(principalIdentity), props);
                        return Redirect(props.RedirectUri);
                    }
                    else
                    {
                        return Redirect("~");
                    }
                }
                else
                {
                    return Challenge(IISDefaults.AuthenticationScheme);
                }
            }
            else
            {
                return Redirect("~");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = CallBackUrl("Account", "VerifyUser", user.Id, code, Request.Scheme);
                    //await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("RegisterConfirmation");
                }
            }
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        public IActionResult LockedOut()
        {
            return View();
        }
    }
}
