using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));
        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IAgencyRepository, AgencyRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<IServiceCalendarRepository, ServiceCalendarRepository>();
        services.AddScoped<IShapeRepository, ShapeRepository>();
        services.AddScoped<IStopRepository, StopRepository>();
        services.AddScoped<IStopTimeRepository, StopTimeRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        return services;
    }
}
