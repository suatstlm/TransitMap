using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DependencyInjection;

namespace Persistence.Extensions;
public static class ApplicationBuilderDbMigrationApplierExtensions
{
    public static IApplicationBuilder UseDbMigrationApplier(this IApplicationBuilder app)
    {
        foreach (IDbMigrationApplierService service in app.ApplicationServices.GetServices<IDbMigrationApplierService>())
        {
            service.Initialize();
        }

        return app;
    }
}
