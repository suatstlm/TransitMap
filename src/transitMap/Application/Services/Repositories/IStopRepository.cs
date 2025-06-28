using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStopRepository : IAsyncRepository<Stop, Guid>, IRepository<Stop, Guid>
{
}