using NArchitecture.Core.Application.Responses;

namespace Application.Features.Trips.Commands.Delete;

public class DeletedTripResponse : IResponse
{
    public Guid Id { get; set; }
}