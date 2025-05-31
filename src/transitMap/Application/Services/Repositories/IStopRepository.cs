using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStopRepository : IAsyncRepository<Stop, Guid>, IRepository<Stop, Guid>
{
}