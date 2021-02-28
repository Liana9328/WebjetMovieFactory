using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebjetMovieFactory.Controllers.ActionFilter
{
    public class TokenAuthenticate : ActionFilterAttribute
    {
        private readonly IConfiguration _config;
        private readonly string _token;

        public TokenAuthenticate(IConfiguration config)
        {
            _config = config;
            _token = _config.GetValue<string>("Token");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var response = context.HttpContext.Response;
            response.Cookies.Append("X-Access-Token", _token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }
    }
}
