using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StopTimes;

public interface IStopTimeService
{
    Task<StopTime?> GetAsync(
        Expression<Func<StopTime, bool>> predicate,
        Func<IQueryable<StopTime>, IIncludableQueryable<StopTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StopTime>?> GetListAsync(
        Expression<Func<StopTime, bool>>? predicate = null,
        Func<IQueryable<StopTime>, IOrderedQueryable<StopTime>>? orderBy = null,
        Func<IQueryable<StopTime>, IIncludableQueryable<StopTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StopTime> AddAsync(StopTime stopTime);
    Task<StopTime> UpdateAsync(StopTime stopTime);
    Task<StopTime> DeleteAsync(StopTime stopTime, bool permanent = false);
}
