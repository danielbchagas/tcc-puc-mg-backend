using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.WebApi.Core.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var erro = new ProblemDetails
                {
                    Detail = e.InnerException?.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Title = e.Message
                };

                await context.Response.WriteAsJsonAsync(erro);
            }
        }
    }
}
