using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };

                var excpetion = new
                {
                    Filter = "HttpResponseExceptionFilter",
                    Source = context.Exception.Source,
                    Message = context.Exception.Message,
                    StackTrace = context.Exception.StackTrace
                };

                var data = new
                {
                    User = context.HttpContext.User.Identity.Name,
                    Hostname = context.HttpContext.Request.Host.ToString(),
                    IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Url = context.HttpContext.Request.GetDisplayUrl(),
                    Method = context.HttpContext.Request.Method,
                    StatusCode = context.HttpContext.Response.StatusCode,
                    AreaAccessed = context.HttpContext.Request.GetDisplayUrl(),
                    Action = context.ActionDescriptor.DisplayName,
                    TimeStamp = DateTime.Now,
                };

                DevTools.PrintConsoleMessage("HttpResponseExceptionFilter Filter  ::::::::::::::::::::::::::::::::::::::::::::", ConsoleColor.Yellow, ConsoleColor.Red);
                DevTools.PrintConsoleMessage(excpetion.ToString(), ConsoleColor.Blue, ConsoleColor.Red);
                DevTools.PrintConsoleMessage(data.ToString(), ConsoleColor.Red, ConsoleColor.Black);
                DevTools.PrintConsoleMessage("", ConsoleColor.Red, ConsoleColor.Black);
                DevTools.PrintConsoleMessage("-------------------------------------------------------------------------", ConsoleColor.Yellow, ConsoleColor.Red);

                context.ExceptionHandled = true;
            }
        }
    }
}
