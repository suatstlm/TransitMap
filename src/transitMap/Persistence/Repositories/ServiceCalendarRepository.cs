using Application.Services.Repositories;
using Domain.Entities;
using Shared.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ServiceCalendarRepository : EfRepositoryBase<ServiceCalendar, Guid, BaseDbContext>, IServiceCalendarRepository
{
    public ServiceCalendarRepository(BaseDbContext context) : base(context)
    {
    }
}