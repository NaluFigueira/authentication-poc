using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using poc_middleware.Infra;

namespace poc_middleware.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateRequest
    {
        private readonly RequestDelegate _next;

        public ValidateRequest(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var apikey = httpContext.Request.Headers["X-API-Key"];

            var validateAPIKey = ApiKey.VerifyApiKey(apikey);

            if (!validateAPIKey)
            {
                httpContext.Response.StatusCode = 401;
                httpContext.Response.ContentType = "text/plain";
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }



            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateRequestExtensions
    {
        public static IApplicationBuilder UseValidateRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateRequest>();
        }
    }
}

