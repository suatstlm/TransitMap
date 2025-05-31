using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITripRepository : IAsyncRepository<Trip, Guid>, IRepository<Trip, Guid>
{
}