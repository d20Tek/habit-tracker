namespace HabitTracker.Web.Common;

internal static class Constants
{
    public const string DefaultErrorCode = "ServiceApi";
    public const string UnexpectedServiceMessage = "Unexpected server error from backend service.";
    public static Error UnexpectedServiceError(string code) => Error.Unexpected(code, UnexpectedServiceMessage);

    public static string UnexpectedRequestMessage(string requestName) =>
        $"Unexpected error... {requestName} request could not be created.";

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

    internal static class Habits
    {
        public const string ServiceUrl = "/api/v1/habit";
        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";
        public static string ServiceUrlWithLimit(int id, int limit) => $"{ServiceUrl}/{id}?limitCompletions={limit}";

        public const string ListUrl = "/habit";
        public const string AddUrl = "/habit/add";

        public static string DetailsUrl(int id) => $"/habit/detail/{id}";
        public static string EditUrl(int id) => $"/habit/edit/{id}";
        public static string DeleteUrl(int id) => $"/habit/delete/{id}";

        public const int NameLength = 100;
        public const int DescLength = 500;
        public const int LimitWeekly = 7;
    }

    internal static class HabitCompletions
    {
        public static string MarkServiceUrl(int id) => $"/api/v1/habit/{id}/mark";
        public static string UnmarkServiceUrl(int id) => $"/api/v1/habit/{id}/unmark";
    }
}
