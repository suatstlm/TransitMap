using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAgencyRepository : IAsyncRepository<Agency, Guid>, IRepository<Agency, Guid>
{
}