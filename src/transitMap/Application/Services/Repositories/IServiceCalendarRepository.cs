using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IServiceCalendarRepository : IAsyncRepository<ServiceCalendar, Guid>, IRepository<ServiceCalendar, Guid>
{
}