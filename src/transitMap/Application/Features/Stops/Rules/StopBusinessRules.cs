using Application.Features.Stops.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Stops.Rules;

public class StopBusinessRules : BaseBusinessRules
{
    private readonly IStopRepository _stopRepository;
    private readonly ILocalizationService _localizationService;

    public StopBusinessRules(IStopRepository stopRepository, ILocalizationService localizationService)
    {
        _stopRepository = stopRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, StopsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task StopShouldExistWhenSelected(Stop? stop)
    {
        if (stop == null)
            await throwBusinessException(StopsBusinessMessages.StopNotExists);
    }

    public async Task StopIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Stop? stop = await _stopRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StopShouldExistWhenSelected(stop);
    }
}