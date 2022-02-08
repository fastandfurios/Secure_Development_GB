using System.Net;
using Debit_Cards_Project.DAL.Exceptions;
using Newtonsoft.Json;

namespace Debit_Cards_Project.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;

            switch (ex)
            {
                case RestException rest:
                    _logger.LogError(ex, "Rest error");
                    errors = rest.Errors;
                    context.Response.StatusCode = (int)rest.Code;
                    break;

                case Exception e:
                    _logger.LogError(e, "Server error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errors is not null)
            {
                var result = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
