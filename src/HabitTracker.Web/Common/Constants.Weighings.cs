using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    internal static partial class Weighings
    {
        public const string ServiceUrl = "/api/v1/weighings";

        public static string ServiceUrlWithId(int id) => $"{ServiceUrl}/{id}";

        public const string ListUrl = "/weighings";

        public static string DisplayTextFormat(DateTimeOffset date, decimal weight) =>
            $"{date:MMM dd, yyyy} - {weight:0.0}";

        public const string DateFormat = "yyyy-MM-dd";

        public const string DisplayDateFormat = "MMM dd, yyyy";

        public const decimal MinWeight = 1;

        public const decimal MaxWeight = 1000;

        public const decimal DefaultStartingWeight = 100;

        public const int Percentage = 100;

        public const decimal MinWeightFactor = 0.5m;

        public const decimal DeltaFactor = 10m;

        public const int WeightGraphMaxColumns = 28;

        public const int GraphAxisLabels = 5;

        public const int GraphAxisFactor = GraphAxisLabels - 1;

        public const string NormalBarCss = "bar";

        public const string SelectedBarCss = "bar selected-bar";
    }
}
