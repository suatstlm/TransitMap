using NArchitecture.Core.Application.Responses;

namespace Application.Features.Stops.Commands.Delete;

public class DeletedStopResponse : IResponse
{
    public Guid Id { get; set; }
}