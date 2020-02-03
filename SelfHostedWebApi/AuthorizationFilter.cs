using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHostedWebApi
{
    public class AuthorizationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (token == null)
                throw new UnauthorizedAccessException("Authorization token must be passed on request");

            var split = token.Split(" ");
        }
    }
}
