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
        public static string ServiceUrlWithLimit(int limit) => $"{ServiceUrl}?limitCompletions={limit}";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";
        public static string ServiceUrlWithLimit(int id, int limit) => $"{ServiceUrl}/{id}?limitCompletions={limit}";

        public const string ListUrl = "/habit";
        public const string AddUrl = "/habit/add";

        public static string DetailsUrl(int id) => $"/habit/detail/{id}";
        public static string EditUrl(int id) => $"/habit/edit/{id}";
        public static string DeleteUrl(int id) => $"/habit/delete/{id}";

        public const string ActiveButton = "active";
        public const string MinDate = "2024-01-01";
        public static string MaxDate = DateTime.Today.ToString("yyyy-MM-dd");

        public const int NameLength = 100;
        public const int DescLength = 500;
        public const int LimitWeekly = 7;
        public const int LimitMonthly = 30;
    }

    internal static class HabitCompletions
    {
        public static string MarkServiceUrl(int id) => $"/api/v1/habit/{id}/mark";
        public static string MarkServiceUrl(int id, int limit) => $"/api/v1/habit/{id}/mark?limitCompletions={limit}";
        public static string UnmarkServiceUrl(int id) => $"/api/v1/habit/{id}/unmark";
        public static string UnmarkServiceUrl(int id, int limit) =>
            $"/api/v1/habit/{id}/unmark?limitCompletions={limit}";

        public static string SuccessMarkIncremented(DateTimeOffset date) =>
            $"Completion incremented by 1 for {date:MMM dd, yyyy}.";
        public static string SuccessUnmarkDecremented(DateTimeOffset date) =>
            $"Completion decremented by 1 for {date:MMM dd, yyyy}.";
    }

    internal static class HabitStatus
    {
        public const string NotStartedColor = "rgb(65,65,65)";
        public const string InProgressColor = "rgb(51,107,57)";
        public const string CompletedColor = "rgb(88,163,79)";
        public const string OverAchievedColor = "lime";
        public const string EmptyColor = "transparent";

        public static string CompletionDisplay(string completion, DateTimeOffset date) =>
            $"{completion} on {date:MMM d}";
    }

    internal static class HabitMonth
    {
        public const int Rows = 6;
        public const int Columns = 7;
        public const int StartRowFull = 5;
        public const int StartRowShort = 4;
    }
}
