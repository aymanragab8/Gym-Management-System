using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace GymSystem.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            int statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;

            string message = exception switch
            {
                DbUpdateException dbEx => ExtractColumnFromConstraint(dbEx),
                _ => exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(new
            {
                status = statusCode,
                message
            });

            return true;
        }

        private static string ExtractColumnFromConstraint(DbUpdateException ex)
        {
            var innerMessage = ex.InnerException?.Message;

            if (string.IsNullOrEmpty(innerMessage))
                return "Database error occurred.";

            var match = Regex.Match(innerMessage, @"constraint '(.+?)'");
            if (match.Success)
            {
                var parts = match.Groups[1].Value.Split('_');
                var columnName = parts.Last();
                return $"{columnName} already exists.";
            }

            return innerMessage;
        }
    }
}