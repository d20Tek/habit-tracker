namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static class Habits
    {
        public const string ServiceUrl = "/api/v1/habits";

        public static string ServiceUrlWithLimit(int limit) => $"{ServiceUrl}?limitCompletions={limit}";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public static string ServiceUrlWithLimit(int id, int limit) => $"{ServiceUrl}/{id}?limitCompletions={limit}";

        public const string ListUrl = "/habits";

        public const string AddUrl = "/habits/add";

        public static string DetailsUrl(int id) => $"/habits/detail/{id}";

        public static string EditUrl(int id) => $"/habits/edit/{id}";

        public static string DeleteUrl(int id) => $"/habits/delete/{id}";

        public const string ActiveButton = "active";

        public const string MinDate = "2024-01-01";

        public static string MaxDate = DateTime.Today.ToString("yyyy-MM-dd");

        public const int NameLength = 100;

        public const int DescLength = 500;

        public const int LimitWeekly = 7;

        public const int LimitMonthly = 30;
    }
}
