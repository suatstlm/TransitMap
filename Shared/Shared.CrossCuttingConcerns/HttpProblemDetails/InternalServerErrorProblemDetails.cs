using Microsoft.AspNetCore.Mvc;

namespace Shared.CrossCuttingConcerns.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails(string detail)
    {
        base.Title = "Internal server error";
        base.Detail = "Internal server error";
        base.Status = 500;
        base.Type = "https://example.com/probs/internal";
    }
}