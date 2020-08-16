﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaxTextile.WebAppCore.Controllers
{

    //[Authorize]
    public abstract class BaseController : Controller
    {
        protected async Task<Dictionary<string, string>> GetDefaultHeaders()
        {
            var headers = new Dictionary<string, string>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            headers.Add("Authorization", "Bearer " + accessToken);
            return headers;
        }
    }
}