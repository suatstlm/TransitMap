using Application.Features.Routes.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Routes.Rules;

public class RouteBusinessRules : BaseBusinessRules
{
    private readonly IRouteRepository _routeRepository;
    private readonly ILocalizationService _localizationService;

    public RouteBusinessRules(IRouteRepository routeRepository, ILocalizationService localizationService)
    {
        _routeRepository = routeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, RoutesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task RouteShouldExistWhenSelected(Route? route)
    {
        if (route == null)
            await throwBusinessException(RoutesBusinessMessages.RouteNotExists);
    }

    public async Task RouteIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Route? route = await _routeRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await RouteShouldExistWhenSelected(route);
    }
}