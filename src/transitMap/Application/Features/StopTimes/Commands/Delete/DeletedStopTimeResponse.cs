using NArchitecture.Core.Application.Responses;

namespace Application.Features.StopTimes.Commands.Delete;

public class DeletedStopTimeResponse : IResponse
{
    public Guid Id { get; set; }
}