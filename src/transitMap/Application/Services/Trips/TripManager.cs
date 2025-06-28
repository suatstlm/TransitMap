using Application.Features.Trips.Rules;
using Application.Services.Repositories;
using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Trips;

public class TripManager : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly TripBusinessRules _tripBusinessRules;

    public TripManager(ITripRepository tripRepository, TripBusinessRules tripBusinessRules)
    {
        _tripRepository = tripRepository;
        _tripBusinessRules = tripBusinessRules;
    }

    public async Task<Trip?> GetAsync(
        Expression<Func<Trip, bool>> predicate,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Trip? trip = await _tripRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return trip;
    }

    public async Task<IPaginate<Trip>?> GetListAsync(
        Expression<Func<Trip, bool>>? predicate = null,
        Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderBy = null,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Trip> tripList = await _tripRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return tripList;
    }

    public async Task<Trip> AddAsync(Trip trip)
    {
        Trip addedTrip = await _tripRepository.AddAsync(trip);

        return addedTrip;
    }

    public async Task<Trip> UpdateAsync(Trip trip)
    {
        Trip updatedTrip = await _tripRepository.UpdateAsync(trip);

        return updatedTrip;
    }

    public async Task<Trip> DeleteAsync(Trip trip, bool permanent = false)
    {
        Trip deletedTrip = await _tripRepository.DeleteAsync(trip);

        return deletedTrip;
    }
}
