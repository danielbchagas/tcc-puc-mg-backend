using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class KissLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<KissLogMiddleware> _logger;

        public KissLogMiddleware(RequestDelegate next, ILogger<KissLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var detalhes = new ProblemDetails
                {
                    Detail = e.InnerException?.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = e.Message,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(detalhes);

                _logger.LogError(e.Message);
            }
        }
    }
}
