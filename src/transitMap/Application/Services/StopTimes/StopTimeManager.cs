using Application.Features.StopTimes.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StopTimes;

public class StopTimeManager : IStopTimeService
{
    private readonly IStopTimeRepository _stopTimeRepository;
    private readonly StopTimeBusinessRules _stopTimeBusinessRules;

    public StopTimeManager(IStopTimeRepository stopTimeRepository, StopTimeBusinessRules stopTimeBusinessRules)
    {
        _stopTimeRepository = stopTimeRepository;
        _stopTimeBusinessRules = stopTimeBusinessRules;
    }

    public async Task<StopTime?> GetAsync(
        Expression<Func<StopTime, bool>> predicate,
        Func<IQueryable<StopTime>, IIncludableQueryable<StopTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StopTime? stopTime = await _stopTimeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return stopTime;
    }

    public async Task<IPaginate<StopTime>?> GetListAsync(
        Expression<Func<StopTime, bool>>? predicate = null,
        Func<IQueryable<StopTime>, IOrderedQueryable<StopTime>>? orderBy = null,
        Func<IQueryable<StopTime>, IIncludableQueryable<StopTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StopTime> stopTimeList = await _stopTimeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return stopTimeList;
    }

    public async Task<StopTime> AddAsync(StopTime stopTime)
    {
        StopTime addedStopTime = await _stopTimeRepository.AddAsync(stopTime);

        return addedStopTime;
    }

    public async Task<StopTime> UpdateAsync(StopTime stopTime)
    {
        StopTime updatedStopTime = await _stopTimeRepository.UpdateAsync(stopTime);

        return updatedStopTime;
    }

    public async Task<StopTime> DeleteAsync(StopTime stopTime, bool permanent = false)
    {
        StopTime deletedStopTime = await _stopTimeRepository.DeleteAsync(stopTime);

        return deletedStopTime;
    }
}
