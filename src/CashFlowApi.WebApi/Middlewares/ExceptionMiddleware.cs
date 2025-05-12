using CashFlowApi.Application.DTOs.Errors;
using CashFlowApi.Application.Exceptions;
using System.Net;
using System.Net.Mime;

namespace CashFlowApi.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }       
        catch (InternalServerException ex)
        {
            _logger.LogError($"InternalServerException: error: {ex.Error}, stackTrace: {ex.StackTrace}");
            await HandleExceptionAsync(httpContext, ex.Error, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            ErrorResponse errorResponse = new()
            {
                Message = "Unexpected error",
                ErrorType = ErrorType.Server,
            };

            _logger.LogCritical("UnexpectedException: errorMessage: {errorMessage}, innerException: {innerException}, stackTrace: {stackTrace}", ex.Message, ex.InnerException, ex.StackTrace);
            await HandleExceptionAsync(httpContext, errorResponse, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, ErrorResponse errorResponse, HttpStatusCode statusCode)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(errorResponse.ToString());
    }
}
