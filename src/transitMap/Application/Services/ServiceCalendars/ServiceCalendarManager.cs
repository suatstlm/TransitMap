using Application.Features.ServiceCalendars.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ServiceCalendars;

public class ServiceCalendarManager : IServiceCalendarService
{
    private readonly IServiceCalendarRepository _serviceCalendarRepository;
    private readonly ServiceCalendarBusinessRules _serviceCalendarBusinessRules;

    public ServiceCalendarManager(IServiceCalendarRepository serviceCalendarRepository, ServiceCalendarBusinessRules serviceCalendarBusinessRules)
    {
        _serviceCalendarRepository = serviceCalendarRepository;
        _serviceCalendarBusinessRules = serviceCalendarBusinessRules;
    }

    public async Task<ServiceCalendar?> GetAsync(
        Expression<Func<ServiceCalendar, bool>> predicate,
        Func<IQueryable<ServiceCalendar>, IIncludableQueryable<ServiceCalendar, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ServiceCalendar? serviceCalendar = await _serviceCalendarRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return serviceCalendar;
    }

    public async Task<IPaginate<ServiceCalendar>?> GetListAsync(
        Expression<Func<ServiceCalendar, bool>>? predicate = null,
        Func<IQueryable<ServiceCalendar>, IOrderedQueryable<ServiceCalendar>>? orderBy = null,
        Func<IQueryable<ServiceCalendar>, IIncludableQueryable<ServiceCalendar, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ServiceCalendar> serviceCalendarList = await _serviceCalendarRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return serviceCalendarList;
    }

    public async Task<ServiceCalendar> AddAsync(ServiceCalendar serviceCalendar)
    {
        ServiceCalendar addedServiceCalendar = await _serviceCalendarRepository.AddAsync(serviceCalendar);

        return addedServiceCalendar;
    }

    public async Task<ServiceCalendar> UpdateAsync(ServiceCalendar serviceCalendar)
    {
        ServiceCalendar updatedServiceCalendar = await _serviceCalendarRepository.UpdateAsync(serviceCalendar);

        return updatedServiceCalendar;
    }

    public async Task<ServiceCalendar> DeleteAsync(ServiceCalendar serviceCalendar, bool permanent = false)
    {
        ServiceCalendar deletedServiceCalendar = await _serviceCalendarRepository.DeleteAsync(serviceCalendar);

        return deletedServiceCalendar;
    }
}
