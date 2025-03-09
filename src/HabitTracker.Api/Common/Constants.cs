using D20Tek.Functional;

namespace HabitTracker.Api.Common;

internal static class Constants
{
    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");
}
