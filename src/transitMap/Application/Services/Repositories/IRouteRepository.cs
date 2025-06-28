using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IRouteRepository : IAsyncRepository<Route, Guid>, IRepository<Route, Guid>
{
}