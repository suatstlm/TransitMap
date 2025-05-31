using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AgencyRepository : EfRepositoryBase<Agency, Guid, BaseDbContext>, IAgencyRepository
{
    public AgencyRepository(BaseDbContext context) : base(context)
    {
    }
}