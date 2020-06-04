using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Domain.Model.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server;
using Security.IdentityServer.Models;

namespace Security.IdentityServer.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly OpenIddictScopeManager<OpenIddictScope<int>> _scopeManager;
        private readonly OpenIddictApplicationManager<OpenIddictApplication<int>> _applicationManager;
        public AuthorizationController(OpenIddictApplicationManager<OpenIddictApplication<int>> manager,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn,
            OpenIddictScopeManager<OpenIddictScope<int>> scopeManager)
        {
            _scopeManager = scopeManager;
            _userManager = userManager;
            _signInManager = signIn;
            _applicationManager = manager;
        }
        [Authorize, HttpGet("~/connect/authorize")]
        public async Task<IActionResult> Authorize(OpenIdConnectRequest openIdConnectRequest)
        {
            var application = await _applicationManager.FindByClientIdAsync(openIdConnectRequest.ClientId);
            if (application == null)
            {
                throw new InvalidOperationException("The application details cannot be found in the database.");
            }

            return await Accept(openIdConnectRequest);
        }
        [HttpGet("~/connect/logout")]
        public async Task<IActionResult> Logout()
        {
            var request = HttpContext.GetOpenIdConnectRequest();
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
            await _signInManager.SignOutAsync();
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = application.PostLogoutRedirectUris
            }, OpenIddictServerDefaults.AuthenticationScheme);
        }
        [Authorize]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(OpenIdConnectRequest openIdConnectRequest)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new InvalidOperationException("An internal error has occurred");
            }
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), OpenIddictServerDefaults.AuthenticationScheme);

            var scopes = openIdConnectRequest.GetScopes().ToImmutableArray();

            ticket.SetScopes(openIdConnectRequest.GetScopes());
            // bu kısım resourceları ticket'a kaydettiğimiz yer.
            ticket.SetResources(await _scopeManager.ListResourcesAsync(scopes));

            foreach (var claim in ticket.Principal.Claims)
            {
                if (claim.Type == "AspNet.Identity.SecurityStamp")
                {
                    continue;
                }
                var destinations = new List<string>();

                if (claim.Type == OpenIdConnectConstants.Claims.Email ||
                    claim.Type == OpenIdConnectConstants.Claims.Name ||
                    claim.Type == OpenIdConnectConstants.Claims.Role)
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.AccessToken);
                }

                if ((claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Scopes.Roles)))
                {
                    if (!destinations.Contains(OpenIdConnectConstants.Destinations.AccessToken))
                    {
                        destinations.Add(OpenIdConnectConstants.Destinations.AccessToken);
                    }
                    if (!destinations.Contains(OpenIdConnectConstants.Destinations.AccessToken))
                    {
                        destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                    }
                }
                claim.SetDestinations(destinations);
            }
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }
    }
}
