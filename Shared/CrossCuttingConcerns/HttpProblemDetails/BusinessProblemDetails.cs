using Microsoft.AspNetCore.Mvc;

namespace CrossCuttingConcerns.HttpProblemDetails;
public class BusinessProblemDetails : ProblemDetails
{
    public BusinessProblemDetails(string detail)
    {
        base.Title = "Rule violation";
        base.Detail = detail;
        base.Status = 400;
        base.Type = "https://example.com/probs/business";
    }
}
