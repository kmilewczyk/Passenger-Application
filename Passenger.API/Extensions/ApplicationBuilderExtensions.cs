using Microsoft.AspNetCore.Diagnostics;
using Passenger.Api.Framework;

namespace Passenger.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ApiExceptionHandlerMiddleware>();
}