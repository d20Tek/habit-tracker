namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static partial class Weighings
    {
        public const string ServiceUrl = "/api/v1/weighing";

        public static string ServiceUrlWithDate(DateTimeOffset date) => $"{ServiceUrl}/{date.Date:MM-dd-yyyy}";

        public const string ListUrl = "/weighing";

        public const decimal MinWeight = 1;

        public const decimal MaxWeight = 1000;
    }
}
