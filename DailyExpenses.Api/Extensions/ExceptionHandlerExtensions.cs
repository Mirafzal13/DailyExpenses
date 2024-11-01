namespace DailyExpenses.Api.Extensions;

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using DailyExpenses.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using FluentValidation.Results;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(config =>
            config.Run(async context =>
                await HandleExceptionAsync(context).ConfigureAwait(false)));

        return app;
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var exception = contextFeature.Error;
            var statusCode = exception.GetStatusCode();
            var problemDetails = GetProblemDetails(exception, (int)statusCode);

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails)).ConfigureAwait(false);

            var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("ExceptionHandler");
            logger.LogError(exception, problemDetails.Title);
        }
    }

    private static ProblemDetails GetProblemDetails(Exception exception, int statusCode)
    {

        var (message, errors) = exception switch
        {
            ValidationException => (exception.Message, ((ValidationException)exception)?.Errors?.ToDictionary()),
            _ => (exception.Message, null),
        };

        return errors == null
            ? new ProblemDetails
            {
                Title = message,
                Status = statusCode
            }
            : new HttpValidationProblemDetails(errors)
            {
                Title = message,
                Status = statusCode,
            };
    }

    private static HttpStatusCode GetStatusCode(this Exception exception)
    {
        return exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            AccessDeniedException => HttpStatusCode.Forbidden,
            ApplicationException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            AlreadyExistsException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };
    }

    private static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> errors)
    {
        return errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray());
    }
}
