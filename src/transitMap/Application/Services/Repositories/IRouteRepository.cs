using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IRouteRepository : IAsyncRepository<Route, Guid>, IRepository<Route, Guid>
{
}