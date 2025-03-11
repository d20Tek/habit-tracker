using D20Tek.Functional;

namespace HabitTracker.Api.Common;

internal static class Constants
{
    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");
}
