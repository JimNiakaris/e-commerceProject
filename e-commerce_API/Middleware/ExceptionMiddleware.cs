using e_commerce_API.Errors;
using System.Net;
using System.Text.Json;

namespace e_commerce_API.Middleware
{
    public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, IHostEnvironment env)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() 
                ? new ApiErrorResponse(httpContext.Response.StatusCode, ex.Message, ex.StackTrace) 
                : new ApiErrorResponse(httpContext.Response.StatusCode,ex.Message, "Internal Server Error");

            var option = new JsonSerializerOptions();
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var json = JsonSerializer.Serialize(response, option);

            return httpContext.Response.WriteAsJsonAsync(json);
        }
    }
}
