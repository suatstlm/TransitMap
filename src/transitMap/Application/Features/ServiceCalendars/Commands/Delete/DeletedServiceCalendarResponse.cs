using NArchitecture.Core.Application.Responses;

namespace Application.Features.ServiceCalendars.Commands.Delete;

public class DeletedServiceCalendarResponse : IResponse
{
    public Guid Id { get; set; }
}