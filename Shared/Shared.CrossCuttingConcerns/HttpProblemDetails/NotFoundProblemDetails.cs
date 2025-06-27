using Microsoft.AspNetCore.Mvc;

namespace Shared.CrossCuttingConcerns.HttpProblemDetails;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(string detail)
    {
        base.Title = "Not found";
        base.Detail = detail;
        base.Status = 404;
        base.Type = "https://example.com/probs/notfound";
    }
}