using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Persistence.DependencyInjection;

public static class ServiceCollectionDbMigrationApplierExtensions
{
    public static IServiceCollection AddDbMigrationApplier<TDbContext>(this IServiceCollection services, Func<ServiceProvider, TDbContext> contextFactory) where TDbContext : DbContext
    {
        Func<ServiceProvider, TDbContext> contextFactory2 = contextFactory;
        ServiceProvider buildServiceProvider = services.BuildServiceProvider();
        services.AddTransient<IDbMigrationApplierService, DbMigrationApplierManager<TDbContext>>((IServiceProvider _) => new DbMigrationApplierManager<TDbContext>(contextFactory2(buildServiceProvider)));
        services.AddTransient<IDbMigrationApplierService<TDbContext>, DbMigrationApplierManager<TDbContext>>((IServiceProvider _) => new DbMigrationApplierManager<TDbContext>(contextFactory2(buildServiceProvider)));
        return services;
    }
}


