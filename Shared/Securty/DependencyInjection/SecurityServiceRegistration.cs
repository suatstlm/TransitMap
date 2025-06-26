using Microsoft.Extensions.DependencyInjection;
using Securty.EmailAuthenticator;
using Securty.JWT;
using Securty.OtpAuthenticator;

namespace Securty.DependencyInjection;
public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices<TUserId, TOperationClaimId, TRefreshTokenId>(this IServiceCollection services, TokenOptions tokenOptions)
    {
        TokenOptions tokenOptions2 = tokenOptions;
        services.AddScoped<ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>, JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId>>((IServiceProvider _) => new JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId>(tokenOptions2));
        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();
        return services;
    }
}