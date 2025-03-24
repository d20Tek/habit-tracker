namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static partial class Weighings
    {
        public const string ServiceUrl = "/api/v1/weighings";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public const string ListUrl = "/weighings";

        public const decimal MinWeight = 1;

        public const decimal MaxWeight = 1000;

        public const int Percentage = 100;

        public const decimal MinWeightFactor = 0.5m;

        public const int WeightGraphMaxColumns = 28;
    }
}
