using NArchitecture.Core.Application.Responses;

namespace Application.Features.Agencies.Commands.Delete;

public class DeletedAgencyResponse : IResponse
{
    public Guid Id { get; set; }
}