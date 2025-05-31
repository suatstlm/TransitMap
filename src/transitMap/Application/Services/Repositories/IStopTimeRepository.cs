using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStopTimeRepository : IAsyncRepository<StopTime, Guid>, IRepository<StopTime, Guid>
{
}