
using Localizations.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Localizations.Extensions;
public static class ApplicationBuilderLocalizationMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseLocalization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LocalizationMiddleware>(Array.Empty<object>());
    }
}
