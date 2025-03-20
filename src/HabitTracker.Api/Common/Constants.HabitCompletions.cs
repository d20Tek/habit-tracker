namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    internal static class HabitCompletions
    {
        public const string MarkServiceBase = "/api/v1/habit/{id:int}/mark";

        public const string MarkName = "MarkHabit";

        public const string MarkDesc = "Mark the habit with a completion count for a date.";

        public const string UnmarkServiceBase = "/api/v1/habit/{id:int}/unmark";

        public const string UnmarkName = "UnmarkHabit";

        public const string UnmarkDesc = "Update the habit to remove a completion count for a date.";

        public static Error IncrementRangeError =
            Error.Validation("HabitCompletion.Increment", "Habit completion increment out of range (1-100).");

        public static Error DecrementRangeError =
            Error.Validation("HabitCompletion.Decrement", "Habit completion decrement out of range (1-100).");

        public static Error FutureDateError =
            Error.Validation("HabitCompletion.Date", "Habit completion date cannot be in the future.");
    }
}
