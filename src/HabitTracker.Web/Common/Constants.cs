namespace HabitTracker.Web.Common;

internal static partial class Constants
{
    public const string DefaultErrorTitle = "Application error";

    public const string DefaultErrorCode = "ServiceApi";

    public const string UnexpectedServiceMessage = "Unexpected server error from backend service.";

    public static Error UnexpectedServiceError(string code) => Error.Unexpected(code, UnexpectedServiceMessage);

    public static Error ServiceRequestError(string code) =>
        Error.Failure(code, "Unable to connect with HabitTracker service. Please try again later.");

    public static string UnexpectedRequestMessage(string requestName) =>
        $"Unexpected error... {requestName} request could not be created.";

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
