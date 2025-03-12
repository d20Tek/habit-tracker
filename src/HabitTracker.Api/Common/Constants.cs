using D20Tek.Functional;

namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    public static Error EntityIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.Id", "Entity id is required.");

    public static Error UserIdRequiredError(string entityType) =>
        Error.Validation($"{entityType}.UserId", "UserId is required.");

    public static Error EntityNotFound(string entityType, int id) => 
        Error.NotFound($"{entityType}.NotFound", $"Entity with id: {id} doesn't exist for user.");

    internal static class Categories
    {
        public const string ServiceBase = "/api/v1/category";
        public const string GetAllName = "GetAllCategories";
        public const string GetByIdName = "GetCategoryById";
        public const string UpdateName = "UpdateCategory";
        public const string CreateName = "CreateCategory";
        public const string DeleteName = "DeleteCategory";
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
