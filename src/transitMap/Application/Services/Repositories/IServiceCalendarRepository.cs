using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IServiceCalendarRepository : IAsyncRepository<ServiceCalendar, Guid>, IRepository<ServiceCalendar, Guid>
{
}