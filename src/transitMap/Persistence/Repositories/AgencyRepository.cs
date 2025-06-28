using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Shared.Persistence.Repositories;

namespace Persistence.Repositories;

public class AgencyRepository : EfRepositoryBase<Agency, Guid, BaseDbContext>, IAgencyRepository
{
    public AgencyRepository(BaseDbContext context) : base(context)
    {
    }
}