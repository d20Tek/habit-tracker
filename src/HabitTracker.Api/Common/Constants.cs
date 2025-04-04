namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    internal class ServiceHealth
    {
        public const string ServiceBase = "/api/v1/health";
    }

    public const string DbConnectionName = "DefaultConnection";

    public static Error EntityIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.Id", "Entity id is required.");

    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error UserIdLengthError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId must be less than 48 characters.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");

    public static void LogEndpointStart<T>(this ILogger<T> logger, string opName) =>
        logger.LogInformation($"==> {opName} called");

    public static void LogEndpointComplete<T>(this ILogger<T> logger, string opName, IResultMonad result) =>
        logger.LogInformation($"==> {opName} complete - result: {result.ToString()}");
}
