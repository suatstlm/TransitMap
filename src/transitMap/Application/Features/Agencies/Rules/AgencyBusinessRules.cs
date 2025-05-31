using Application.Features.Agencies.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Agencies.Rules;

public class AgencyBusinessRules : BaseBusinessRules
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly ILocalizationService _localizationService;

    public AgencyBusinessRules(IAgencyRepository agencyRepository, ILocalizationService localizationService)
    {
        _agencyRepository = agencyRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AgenciesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AgencyShouldExistWhenSelected(Agency? agency)
    {
        if (agency == null)
            await throwBusinessException(AgenciesBusinessMessages.AgencyNotExists);
    }

    public async Task AgencyIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Agency? agency = await _agencyRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AgencyShouldExistWhenSelected(agency);
    }
}