using HabitTracker.Api.Features.Categories;

namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    internal static class Categories
    {
        public const string ServiceBase = "/api/v1/categories";

        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";

        public const string GetAllName = "GetAllCategories";

        public const string GetAllDesc = "Retrieves a list of categories for the logged in user.";

        public const string GetByIdName = "GetCategoryById";

        public const string GetByIdDesc = "Retrieves a single category identified by its unique id.";

        public const string CreateName = "CreateCategory";

        public const string CreateDesc = "Creates a new category with data from the message body.";

        public const string UpdateName = "UpdateCategory";

        public const string UpdateDesc =
            "Modifies a single category identified by its unique id with data from the message body.";

        public const string DeleteName = "DeleteCategory";

        public const string DeleteDesc = "Deletes a single category identified by its unique id.";

        public const int NameLength = 100;

        public const int UserIdLength = 48;

        public static Error RequiredNameError = Error.Validation("Category.Name", "Category name is a required.");

        public static Error NameLengthError =
            Error.Validation("Category.Name", "Category name must be less than 100 characters.");

        public static string GetCacheKey(string userId) => $"categories_for_{userId}";

        public static string GetByIdCacheKey(int catId, string userId) => $"category_{catId}_for_{userId}";

        public static TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);
    }
}
