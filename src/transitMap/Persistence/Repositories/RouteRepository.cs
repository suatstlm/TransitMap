using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RouteRepository : EfRepositoryBase<Route, Guid, BaseDbContext>, IRouteRepository
{
    public RouteRepository(BaseDbContext context) : base(context)
    {
    }
}