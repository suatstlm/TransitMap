using Application.Features.Trips.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Trips.Rules;

public class TripBusinessRules : BaseBusinessRules
{
    private readonly ITripRepository _tripRepository;
    private readonly ILocalizationService _localizationService;

    public TripBusinessRules(ITripRepository tripRepository, ILocalizationService localizationService)
    {
        _tripRepository = tripRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TripsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TripShouldExistWhenSelected(Trip? trip)
    {
        if (trip == null)
            await throwBusinessException(TripsBusinessMessages.TripNotExists);
    }

    public async Task TripIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Trip? trip = await _tripRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TripShouldExistWhenSelected(trip);
    }
}