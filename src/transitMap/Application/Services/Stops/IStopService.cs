using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Stops;

public interface IStopService
{
    Task<Stop?> GetAsync(
        Expression<Func<Stop, bool>> predicate,
        Func<IQueryable<Stop>, IIncludableQueryable<Stop, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Stop>?> GetListAsync(
        Expression<Func<Stop, bool>>? predicate = null,
        Func<IQueryable<Stop>, IOrderedQueryable<Stop>>? orderBy = null,
        Func<IQueryable<Stop>, IIncludableQueryable<Stop, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Stop> AddAsync(Stop stop);
    Task<Stop> UpdateAsync(Stop stop);
    Task<Stop> DeleteAsync(Stop stop, bool permanent = false);
}
