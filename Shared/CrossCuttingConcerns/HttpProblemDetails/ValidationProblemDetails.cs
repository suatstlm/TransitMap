﻿using CrossCuttingConcerns.Exception.Types;
using Microsoft.AspNetCore.Mvc;

namespace CrossCuttingConcerns.HttpProblemDetails;

public class ValidationProblemDetails : ProblemDetails
{
    public IEnumerable<ValidationExceptionModel> Errors { get; init; }

    public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
    {
        base.Title = "Validation error(s)";
        base.Detail = "One or more validation errors occurred.";
        Errors = errors;
        base.Status = 400;
        base.Type = "https://example.com/probs/validation";
    }
}
