using Application.Services.Repositories;
using Domain.Entities;
using Shared.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StopRepository : EfRepositoryBase<Stop, Guid, BaseDbContext>, IStopRepository
{
    public StopRepository(BaseDbContext context) : base(context)
    {
    }
}