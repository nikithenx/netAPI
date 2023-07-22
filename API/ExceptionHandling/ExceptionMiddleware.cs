using System.Net;

namespace API.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                await response.WriteAsync(new ExceptionDetails()
                {
                    StatusCode = response.StatusCode,
                    Message = error.Message
                }.ToString());
            }
        }
    }
}