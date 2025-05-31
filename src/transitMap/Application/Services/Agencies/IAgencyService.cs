using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Agencies;

public interface IAgencyService
{
    Task<Agency?> GetAsync(
        Expression<Func<Agency, bool>> predicate,
        Func<IQueryable<Agency>, IIncludableQueryable<Agency, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Agency>?> GetListAsync(
        Expression<Func<Agency, bool>>? predicate = null,
        Func<IQueryable<Agency>, IOrderedQueryable<Agency>>? orderBy = null,
        Func<IQueryable<Agency>, IIncludableQueryable<Agency, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Agency> AddAsync(Agency agency);
    Task<Agency> UpdateAsync(Agency agency);
    Task<Agency> DeleteAsync(Agency agency, bool permanent = false);
}
