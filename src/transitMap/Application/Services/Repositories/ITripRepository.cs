using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITripRepository : IAsyncRepository<Trip, Guid>, IRepository<Trip, Guid>
{
}