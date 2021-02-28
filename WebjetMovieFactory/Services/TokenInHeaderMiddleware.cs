using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebjetMovieFactory.Services
{
    public class TokenInHeaderMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public TokenInHeaderMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            var cookie = context.Request.Cookies["X-Access-Token"];

            if (cookie != null)
                if (!context.Request.Headers.ContainsKey("Authorization"))
                    context.Request.Headers.Append("Authorization", "Bearer " + cookie);

            await _requestDelegate.Invoke(context);
        }
    }
}
