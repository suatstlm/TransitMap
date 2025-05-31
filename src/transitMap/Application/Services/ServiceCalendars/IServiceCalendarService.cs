using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ServiceCalendars;

public interface IServiceCalendarService
{
    Task<ServiceCalendar?> GetAsync(
        Expression<Func<ServiceCalendar, bool>> predicate,
        Func<IQueryable<ServiceCalendar>, IIncludableQueryable<ServiceCalendar, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ServiceCalendar>?> GetListAsync(
        Expression<Func<ServiceCalendar, bool>>? predicate = null,
        Func<IQueryable<ServiceCalendar>, IOrderedQueryable<ServiceCalendar>>? orderBy = null,
        Func<IQueryable<ServiceCalendar>, IIncludableQueryable<ServiceCalendar, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ServiceCalendar> AddAsync(ServiceCalendar serviceCalendar);
    Task<ServiceCalendar> UpdateAsync(ServiceCalendar serviceCalendar);
    Task<ServiceCalendar> DeleteAsync(ServiceCalendar serviceCalendar, bool permanent = false);
}
