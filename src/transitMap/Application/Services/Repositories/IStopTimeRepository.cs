using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStopTimeRepository : IAsyncRepository<StopTime, Guid>, IRepository<StopTime, Guid>
{
}