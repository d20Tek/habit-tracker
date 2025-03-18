namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    public static Error EntityIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.Id", "Entity id is required.");

    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");

    public static void LogEndpointStart<T>(this ILogger<T> logger, string opName) =>
        logger.LogInformation($"==> {Constants.Categories.GetAllName} called");

    public static void LogEndpointComplete<T>(this ILogger<T> logger, string opName, IResultMonad result) =>
        logger.LogInformation($"==> {Constants.Categories.GetAllName} complete - result: {result.ToString()}");

    internal static class Categories
    {
        public const string ServiceBase = "/api/v1/category";
        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";
        public const string GetAllName = "GetAllCategories";
        public const string GetAllDesc = "Retrieves a list of categories for the logged in user.";
        public const string GetByIdName = "GetCategoryById";
        public const string GetByIdDesc = "Retrieves a single category identified by its unique id.";
        public const string CreateName = "CreateCategory";
        public const string CreateDesc = "Creates a new category with data from the message body.";
        public const string UpdateName = "UpdateCategory";
        public const string UpdateDesc = "Modifies a single category identified by its unique id with data from the message body.";
        public const string DeleteName = "DeleteCategory";
        public const string DeleteDesc = "Deletes a single category identified by its unique id.";
        public const int NameLength = 100;
        public const int UserIdLength = 48;

        public static Error RequiredNameError = Error.Validation("Category.Name", "Category name is a required.");
        public static Error NameLengthError = Error.Validation("Category.Name", "Category name must be less than 100 characters.");
    }

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
        public const string UpdateDesc = "Modifies a habit identified by its unique id with data from the message body.";
        public const string DeleteName = "DeleteHabit";
        public const string DeleteDesc = "Deletes a single habit identified by its unique id.";
        public const int NameLength = 100;
        public const int DescLength = 500;
        public const int UserIdLength = 48;

        public static Error RequiredNameError = Error.Validation("Habit.Name", "Habit name is a required.");
        public static Error NameLengthError = Error.Validation("Habit.Name", "Habit name must be less than 100 characters.");
        public static Error DescLengthError = Error.Validation("Habit.Description", "Habit description must be less than 500 characters.");
        public static Error TargetAttemptsError = Error.Validation("Habit.TargetAttempts", "Target attempts must be greater than 0.");
        public static Error CategoryIdError = Error.Validation("Habit.CategoryId", "Habit's CategoryId must be greater than 0.");
    }

    internal static class HabitCompletions
    {
        public const string MarkServiceBase = "/api/v1/habit/{id:int}/mark";
        public const string MarkName = "MarkHabit";
        public const string MarkDesc = "Mark the habit with a completion count for a date.";

        public const string UnmarkServiceBase = "/api/v1/habit/{id:int}/unmark";
        public const string UnmarkName = "UnmarkHabit";
        public const string UnmarkDesc = "Update the habit to remove a completion count for a date.";

        public static Error IncrementRangeError = Error.Validation("HabitCompletion.Increment", "Habit completion increment out of range (1-100).");
        public static Error DecrementRangeError = Error.Validation("HabitCompletion.Decrement", "Habit completion decrement out of range (1-100).");
        public static Error FutureDateError = Error.Validation("HabitCompletion.Date", "Habit completion date cannot be in the future.");
    }
}
