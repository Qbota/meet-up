using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebApplication.Exceptions;

namespace WebApplication.Middleware
{
    public class MeetUpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MeetUpMiddleware> _logger;

        public MeetUpMiddleware(RequestDelegate next,
            ILogger<MeetUpMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the exception middleware will not be executed");
                    throw;
                }
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Instance = context.Request.Path
                };
                switch (exception)
                {
                    case InvalidOperationException invalidOperationException:
                        HandleException(ref problemDetails, invalidOperationException.InnerException ?? invalidOperationException, context);
                        break;
                   default:
                        HandleException(ref problemDetails, exception, context);
                        break;
                }
                _logger.LogDebug($"Exception {exception.GetType().FullName} occured: {exception}");
                context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = context.Response.ContentType ?? "application/json";

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize<ProblemDetails>(problemDetails));
            }
        }

        private void HandleException(ref ProblemDetails problemDetails, Exception exception, HttpContext context)
        {
            switch (exception)
            {
                case BadHttpRequestException badHttpRequestException:
                    HandleException(ref problemDetails, badHttpRequestException, context);
                    break;
                case NotImplementedException notImplementedException:
                    HandleException(ref problemDetails, notImplementedException, context);
                    break;
                case AuthorizationException authorizationException:
                    HandleException(ref problemDetails, authorizationException, context);
                    break;
                case AuthenticationException authenticationException:
                    HandleException(ref problemDetails, authenticationException, context);
                    break;
                default:
                    problemDetails.Title = "An unexpected error occured";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.ToString();
                    break;
            }
        }
        private void HandleException(ref ProblemDetails problemDetails, BadHttpRequestException badHttpRequestException, HttpContext context)
        {
            problemDetails.Title = "Invalid request";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = badHttpRequestException.Message;
        }
        private void HandleException(ref ProblemDetails problemDetails, NotImplementedException notImplementedException, HttpContext context)
        {
            problemDetails.Title = "Feature is not implemented";
            problemDetails.Status = StatusCodes.Status501NotImplemented;
            problemDetails.Detail = notImplementedException.Message;
        }
        private void HandleException(ref ProblemDetails problemDetails, AuthenticationException authenticationException, HttpContext context)
        {
            problemDetails.Title = "Unauthenitcated operation";
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Detail = authenticationException.Message;
        }
        private void HandleException(ref ProblemDetails problemDetails, AuthorizationException authorizationException, HttpContext context)
        {
            problemDetails.Title = "Unauthenitcated operation";
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Detail = authorizationException.Message;
        }

    }
}
