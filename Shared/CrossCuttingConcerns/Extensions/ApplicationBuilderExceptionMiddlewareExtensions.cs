
using CrossCuttingConcerns.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CrossCuttingConcerns.Extensions;
public static class ApplicationBuilderExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>(Array.Empty<object>());
    }
}
