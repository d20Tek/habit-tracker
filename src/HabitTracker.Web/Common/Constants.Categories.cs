namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static class Categories
    {
        public const string ServiceUrl = "/api/v1/categories";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public const string ListUrl = "/categories";

        public const string AddUrl = "/categories/add";

        public static string EditUrl(int id) => $"/categories/edit/{id}";

        public static string DeleteUrl(int id) => $"/categories/delete/{id}";

        public const int NameLength = 100;
    }
}
