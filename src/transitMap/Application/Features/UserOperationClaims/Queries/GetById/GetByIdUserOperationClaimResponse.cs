using Shared.Application.Responses;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }
}
