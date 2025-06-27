using Applicaton.Pipelines.Logging;
using Shared.CrossCuttingConcerns.Handlers;
using Shared.CrossCuttingConcerns.Logging.Abstraction;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Shared.CrossCuttingConcerns.Middleware;
public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;

    private readonly HttpExceptionHandler _httpExceptionHandler;

    private readonly ILogger _loggerService;

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger loggerService)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _loggerService = loggerService;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    protected virtual Task HandleExceptionAsync(HttpResponse response, dynamic exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleException(exception);
    }

    protected virtual Task LogException(HttpContext context, System.Exception exception)
    {
        List<LogParameter> list = new List<LogParameter>();
        CollectionsMarshal.SetCount(list, 1);
        Span<LogParameter> span = CollectionsMarshal.AsSpan(list);
        int num = 0;
        span[num] = new LogParameter
        {
            Type = context.GetType().Name,
            Value = exception.ToString()
        };
        num++;
        List<LogParameter> parameters = list;
        LogDetail value = new LogDetail
        {
            MethodName = _next.Method.Name,
            Parameters = parameters,
            User = (_contextAccessor.HttpContext?.User.Identity?.Name ?? "?")
        };
        _loggerService.Information(JsonSerializer.Serialize(value));
        return Task.CompletedTask;
    }
}
