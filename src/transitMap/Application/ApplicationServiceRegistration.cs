using System.Reflection;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.ElasticSearch;
using Shared.ElasticSearch.Models;
using Shared.Mailing;
using Shared.Mailing.MailKit;
using Application.Services.Agencies;
using Application.Services.Routes;
using Application.Services.ServiceCalendars;
using Application.Services.Shapes;
using Application.Services.Stops;
using Application.Services.StopTimes;
using Application.Services.Trips;
using Shared.Security.JWT;
using Shared.CrossCuttingConcerns.Logging.Configurations;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Validation;
using Shared.Application.Pipelines.Transaction;
using Shared.Application.Rules;
using Shared.CrossCuttingConcerns.Logging.Serilog.File;
using Shared.CrossCuttingConcerns.Logging.Abstraction;
using Shared.Security.DependencyInjection;
using Shared.Localizations.Resource.Yaml.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        MailSettings mailSettings,
        FileLogConfiguration fileLogConfiguration,
        ElasticSearchConfig elasticSearchConfig,
        TokenOptions tokenOptions
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));
        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));
        services.AddSingleton<IElasticSearch, ElasticSearchManager>(_ => new ElasticSearchManager(elasticSearchConfig));

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();

        services.AddYamlResourceLocalization();

        services.AddSecurityServices<Guid, int, Guid>(tokenOptions);

        services.AddScoped<IAgencyService, AgencyManager>();
        services.AddScoped<IRouteService, RouteManager>();
        services.AddScoped<IServiceCalendarService, ServiceCalendarManager>();
        services.AddScoped<IShapeService, ShapeManager>();
        services.AddScoped<IStopService, StopManager>();
        services.AddScoped<IStopTimeService, StopTimeManager>();
        services.AddScoped<ITripService, TripManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
