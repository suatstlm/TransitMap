using Shared.Localizations.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Shared.Localizations.Extensions;
public static class ApplicationBuilderLocalizationMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseLocalization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LocalizationMiddleware>(Array.Empty<object>());
    }
}
