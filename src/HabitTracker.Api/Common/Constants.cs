namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    public static Error EntityIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.Id", "Entity id is required.");

    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");

    public const string EndpointSuccess = "succeeded";
    public static void LogEndpointStart<T>(this ILogger<T> logger, string opName) =>
        logger.LogInformation($"==> {Constants.Categories.GetAllName} called");
    public static void LogEndpointComplete<T>(this ILogger<T> logger, string opName, string message) =>
        logger.LogInformation($"==> {Constants.Categories.GetAllName} complete - result: {message}");

    internal static class Categories
    {
        public const string ServiceBase = "/api/v1/category";
        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";
        public const string GetAllName = "GetAllCategories";
        public const string GetAllDesc = "Retrieves a list of categories for the logged in user.";
        public const string GetByIdName = "GetCategoryById";
        public const string GetByIdDesc = "Retrieves a single category identified by its unique id.";
        public const string CreateName = "CreateCategory";
        public const string CreateDesc = "Create a new category with data from the message body.";
        public const string UpdateName = "UpdateCategory";
        public const string UpdateDesc = "Modifies a single category identified by its unique id with data from the message body.";
        public const string DeleteName = "DeleteCategory";
        public const string DeleteDesc = "Deletes a single category identified by its unique id.";
        public const int NameLength = 100;
        public const int UserIdLength = 32;

        public static Error RequiredNameError = Error.Validation("CreateCategory.Name", "Category name is a required.");
        public static Error NameLengthError = Error.Validation("CreateCategory.Name", "Category name must be less than 100 characters.");
    }

    internal static class Habits
    {
        public const int NameLength = 100;
        public const int DescLength = 500;
        public const int UserIdLength = 32;
    }
}
