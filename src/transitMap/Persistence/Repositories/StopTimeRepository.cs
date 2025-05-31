using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StopTimeRepository : EfRepositoryBase<StopTime, Guid, BaseDbContext>, IStopTimeRepository
{
    public StopTimeRepository(BaseDbContext context) : base(context)
    {
    }
}