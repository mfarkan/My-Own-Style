using AspNet.Security.OAuth.Introspection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HasTextile.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
    }
}
