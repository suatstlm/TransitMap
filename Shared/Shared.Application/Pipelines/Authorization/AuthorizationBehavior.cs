using MediatR;
using Microsoft.AspNetCore.Http;
using CrossCuttingConcerns.Exception.Types;
using Securty.Extensions;

namespace Shared.Applicaton.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TRequest request2 = request;
        if (string.IsNullOrEmpty((_httpContextAccessor.HttpContext.User.GetRoleClaims() ?? throw new AuthorizationException("You are not authenticated.")).FirstOrDefault(userRoleClaim => userRoleClaim == "Admin" || request2.Roles.Contains(userRoleClaim))))
        {
            throw new AuthorizationException("You are not authorized.");
        }

        return await next();
    }
}
