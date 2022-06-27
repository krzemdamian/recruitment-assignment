using Common.Domain.ActionContext;
using Common.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class ActionContextMiddleware : IMiddleware
    {
        private readonly IActionContextProvider actionContextProvider;

        public ActionContextMiddleware(IActionContextProvider actionContextProvider)
        {
            this.actionContextProvider = actionContextProvider;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userIdString = context.Request.Headers["userId"].ToString();

            var userId = UserId.Void;

            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userIdGuid))
            {
                userId = UserId.CreateOrNull(userIdGuid);
            }

            var actionContext = new ActionContext(userId);
            this.actionContextProvider.RegisterContext(actionContext);

            return next(context);
        }
    }
}
