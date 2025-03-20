namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static class Categories
    {
        public const string ServiceUrl = "/api/v1/category";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public const string ListUrl = "/category";

        public const string AddUrl = "/category/add";

        public static string EditUrl(int id) => $"/category/edit/{id}";

        public static string DeleteUrl(int id) => $"/category/delete/{id}";

        public const int NameLength = 100;
    }
}
