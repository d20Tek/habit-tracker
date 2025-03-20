namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    internal static class Habits
    {
        public const string ServiceBase = "/api/v1/habit";

        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";

        public const string GetAllName = "GetAllHabits";

        public const string GetAllDesc = "Retrieves a list of habits for the logged in user.";

        public const string GetByIdName = "GetHabitById";

        public const string GetByIdDesc = "Retrieves a single habit identified by its unique id.";

        public const string CreateName = "CreateHabit";

        public const string CreateDesc = "Creates a new habit with data from the message body.";

        public const string UpdateName = "UpdateHabit";

        public const string UpdateDesc =

            "Modifies a habit identified by its unique id with data from the message body.";

        public const string DeleteName = "DeleteHabit";

        public const string DeleteDesc = "Deletes a single habit identified by its unique id.";

        public const int NameLength = 100;

        public const int DescLength = 500;

        public const int UserIdLength = 48;

        public static Error RequiredNameError = Error.Validation("Habit.Name", "Habit name is a required.");

        public static Error NameLengthError =
            Error.Validation("Habit.Name", "Habit name must be less than 100 characters.");

        public static Error DescLengthError =
            Error.Validation("Habit.Description", "Habit description must be less than 500 characters.");

        public static Error TargetAttemptsError =
            Error.Validation("Habit.TargetAttempts", "Target attempts must be greater than 0.");

        public static Error CategoryIdError =
            Error.Validation("Habit.CategoryId", "Habit's CategoryId must be greater than 0.");
    }
}
