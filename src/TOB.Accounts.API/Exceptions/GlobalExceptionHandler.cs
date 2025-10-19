using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TOB.Accounts.API.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "An unhandled exception occurred. TraceId: {TraceId}",
            httpContext.TraceIdentifier);

        var (statusCode, title, detail) = exception switch
        {
            NotFoundException notFoundException => (
                HttpStatusCode.NotFound,
                "Resource Not Found",
                notFoundException.Message
            ),
            ValidationException validationException => (
                HttpStatusCode.BadRequest,
                "Validation Error",
                validationException.Message
            ),
            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Unauthorized",
                "You are not authorized to access this resource"
            ),
            ArgumentException argumentException => (
                HttpStatusCode.BadRequest,
                "Invalid Argument",
                argumentException.Message
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred. Please try again later."
            )
        };

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path,
            Type = $"https://httpstatuses.com/{(int)statusCode}"
        };

        // Add TraceId for debugging
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        // Add timestamp
        problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);

        // Include validation errors if present
        if (exception is ValidationException validationEx && validationEx.Errors.Any())
        {
            problemDetails.Extensions.Add("errors", validationEx.Errors);
        }

        httpContext.Response.StatusCode = (int)statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
