using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace WebjetMovieFactory.Controllers.ActionFilter
{
    public class ResponseExceptionFilter : ActionFilterAttribute
    {
        private readonly ILogger<ResponseExceptionFilter> _logger;

        public ResponseExceptionFilter(ILogger<ResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;

                _logger.LogError($"Exception thrown: {exception.Message}");
            }
        }
    }
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
