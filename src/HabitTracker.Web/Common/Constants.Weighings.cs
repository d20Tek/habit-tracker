namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static partial class Weighings
    {
        public const string ServiceUrl = "/api/v1/weighing";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public const string ListUrl = "/weighing";

        public const decimal MinWeight = 1;

        public const decimal MaxWeight = 1000;
    }
}
