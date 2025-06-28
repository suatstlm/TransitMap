using Application.Features.StopTimes.Constants;
using Application.Services.Repositories;
using Shared.Application.Rules;
using Shared.CrossCuttingConcerns.Exception.Types;
using Shared.Localizations.Abstraction;
using Domain.Entities;

namespace Application.Features.StopTimes.Rules;

public class StopTimeBusinessRules : BaseBusinessRules
{
    private readonly IStopTimeRepository _stopTimeRepository;
    private readonly ILocalizationService _localizationService;

    public StopTimeBusinessRules(IStopTimeRepository stopTimeRepository, ILocalizationService localizationService)
    {
        _stopTimeRepository = stopTimeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, StopTimesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task StopTimeShouldExistWhenSelected(StopTime? stopTime)
    {
        if (stopTime == null)
            await throwBusinessException(StopTimesBusinessMessages.StopTimeNotExists);
    }

    public async Task StopTimeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StopTime? stopTime = await _stopTimeRepository.GetAsync(
            predicate: st => st.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StopTimeShouldExistWhenSelected(stopTime);
    }
}