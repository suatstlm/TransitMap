using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Trips;

public interface ITripService
{
    Task<Trip?> GetAsync(
        Expression<Func<Trip, bool>> predicate,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Trip>?> GetListAsync(
        Expression<Func<Trip, bool>>? predicate = null,
        Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderBy = null,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Trip> AddAsync(Trip trip);
    Task<Trip> UpdateAsync(Trip trip);
    Task<Trip> DeleteAsync(Trip trip, bool permanent = false);
}
