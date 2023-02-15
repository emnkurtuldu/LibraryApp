using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimApp.Persistence.Exceptions
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now.ToString("HH:mm:ss")} : {ex}");
                _logger.LogError($"Message : {ex.Message}");
                _logger.LogError($"StackTrace : {ex.StackTrace}");
                await Task.CompletedTask;
            }
        }

    }
}
