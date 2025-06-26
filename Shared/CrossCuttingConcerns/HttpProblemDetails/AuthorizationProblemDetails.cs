﻿using Microsoft.AspNetCore.Mvc;

namespace CrossCuttingConcerns.HttpProblemDetails;

public class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        base.Title = "Authorization error";
        base.Detail = detail;
        base.Status = 401;
        base.Type = "https://example.com/probs/authorization";
    }
}