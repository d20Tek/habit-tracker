namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static class HabitCompletions
    {
        public static string MarkServiceUrl(int id) => $"/api/v1/habits/{id}/mark";

        public static string MarkServiceUrl(int id, int limit) => $"/api/v1/habits/{id}/mark?limitCompletions={limit}";

        public static string UnmarkServiceUrl(int id) => $"/api/v1/habits/{id}/unmark";

        public static string UnmarkServiceUrl(int id, int limit) =>
            $"/api/v1/habits/{id}/unmark?limitCompletions={limit}";

        public static string SuccessMarkIncremented(DateTimeOffset date) =>
            $"Completion incremented by 1 for {date:MMM dd, yyyy}.";

        public static string SuccessUnmarkDecremented(DateTimeOffset date) =>
            $"Completion decremented by 1 for {date:MMM dd, yyyy}.";
    }
}
