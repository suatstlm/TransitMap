using Application.Services.Repositories;
using Domain.Entities;
using Shared.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TripRepository : EfRepositoryBase<Trip, Guid, BaseDbContext>, ITripRepository
{
    public TripRepository(BaseDbContext context) : base(context)
    {
    }
}