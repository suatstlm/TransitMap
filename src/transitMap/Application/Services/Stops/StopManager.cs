using Application.Features.Stops.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Stops;

public class StopManager : IStopService
{
    private readonly IStopRepository _stopRepository;
    private readonly StopBusinessRules _stopBusinessRules;

    public StopManager(IStopRepository stopRepository, StopBusinessRules stopBusinessRules)
    {
        _stopRepository = stopRepository;
        _stopBusinessRules = stopBusinessRules;
    }

    public async Task<Stop?> GetAsync(
        Expression<Func<Stop, bool>> predicate,
        Func<IQueryable<Stop>, IIncludableQueryable<Stop, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Stop? stop = await _stopRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return stop;
    }

    public async Task<IPaginate<Stop>?> GetListAsync(
        Expression<Func<Stop, bool>>? predicate = null,
        Func<IQueryable<Stop>, IOrderedQueryable<Stop>>? orderBy = null,
        Func<IQueryable<Stop>, IIncludableQueryable<Stop, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Stop> stopList = await _stopRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return stopList;
    }

    public async Task<Stop> AddAsync(Stop stop)
    {
        Stop addedStop = await _stopRepository.AddAsync(stop);

        return addedStop;
    }

    public async Task<Stop> UpdateAsync(Stop stop)
    {
        Stop updatedStop = await _stopRepository.UpdateAsync(stop);

        return updatedStop;
    }

    public async Task<Stop> DeleteAsync(Stop stop, bool permanent = false)
    {
        Stop deletedStop = await _stopRepository.DeleteAsync(stop);

        return deletedStop;
    }
}
