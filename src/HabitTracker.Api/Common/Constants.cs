using D20Tek.Functional;

namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");

    internal static class Categories
    {
        public const int NameLength = 100;
        public const int UserIdLength = 32;
    }

    internal static class Habits
    {
        public const int NameLength = 100;
        public const int DescLength = 500;
        public const int UserIdLength = 32;
    }
}
