using Application.Features.ServiceCalendars.Constants;
using Application.Services.Repositories;
using Shared.Application.Rules;
using Shared.CrossCuttingConcerns.Exception.Types;
using Shared.Localizations.Abstraction;
using Domain.Entities;

namespace Application.Features.ServiceCalendars.Rules;

public class ServiceCalendarBusinessRules : BaseBusinessRules
{
    private readonly IServiceCalendarRepository _serviceCalendarRepository;
    private readonly ILocalizationService _localizationService;

    public ServiceCalendarBusinessRules(IServiceCalendarRepository serviceCalendarRepository, ILocalizationService localizationService)
    {
        _serviceCalendarRepository = serviceCalendarRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ServiceCalendarsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ServiceCalendarShouldExistWhenSelected(ServiceCalendar? serviceCalendar)
    {
        if (serviceCalendar == null)
            await throwBusinessException(ServiceCalendarsBusinessMessages.ServiceCalendarNotExists);
    }

    public async Task ServiceCalendarIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ServiceCalendar? serviceCalendar = await _serviceCalendarRepository.GetAsync(
            predicate: sc => sc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ServiceCalendarShouldExistWhenSelected(serviceCalendar);
    }
}