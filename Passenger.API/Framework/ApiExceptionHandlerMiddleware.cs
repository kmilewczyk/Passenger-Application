
using System.Net;
using System.Text.Json;
using Passenger.Infrastructure.Exceptions;

namespace Passenger.Api.Framework;

public class ApiExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;


    public ApiExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorCode = "error";
        var statusCode = HttpStatusCode.BadRequest;
        var exceptionType = exception.GetType();
        switch (exception)
        {
            case { } e when exceptionType == typeof(UnauthorizedAccessException):
                statusCode = HttpStatusCode.Unauthorized;
                break;
            case ServiceException e when exceptionType == typeof(ServiceException):
                statusCode = HttpStatusCode.BadRequest;
                errorCode = e.Code;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        var response = new {code = errorCode, message = exception.Message};
        var payload = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) statusCode;

        return context.Response.WriteAsync(payload);
    }
}