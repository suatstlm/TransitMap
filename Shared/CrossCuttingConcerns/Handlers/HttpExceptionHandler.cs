using CrossCuttingConcerns.Exception.Types;
using CrossCuttingConcerns.Extensions;
using CrossCuttingConcerns.HttpProblemDetails;
using Microsoft.AspNetCore.Http;

namespace CrossCuttingConcerns.Handlers;
public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;

    public HttpResponse Response
    {
        get
        {
            return _response ?? throw new NullReferenceException("_response");
        }
        set
        {
            _response = value;
        }
    }

    public override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = 400;
        string text = new BusinessProblemDetails(businessException.Message).ToJson();
        return Response.WriteAsync(text);
    }

    public override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = 400;
        string text = new ValidationProblemDetails(validationException.Errors).ToJson();
        return Response.WriteAsync(text);
    }

    public override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = 401;
        string text = new AuthorizationProblemDetails(authorizationException.Message).ToJson();
        return Response.WriteAsync(text);
    }

    public override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = 404;
        string text = new NotFoundProblemDetails(notFoundException.Message).ToJson();
        return Response.WriteAsync(text);
    }

    public override Task HandleException(System.Exception exception)
    {
        Response.StatusCode = 500;
        string text = new InternalServerErrorProblemDetails(exception.Message).ToJson();
        return Response.WriteAsync(text);
    }
}
