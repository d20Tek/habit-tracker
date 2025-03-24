namespace HabitTracker.Api.Common;

internal partial class Constants
{
    internal static class Weighings
    {
        public const string ServiceBase = "/api/v1/weighings";

        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";

        public const string GetAllName = "GetAllWeighings";

        public const string GetAllDesc = "Retrieves a list of weighings for the user.";

        public const string GetByIdName = "GetWeighingById";

        public const string GetByIdDesc = "Retrieves a single weighing identified by its id.";

        public const string UpsertName = "UpsertWeighing";

        public const string UpsertDesc =
            "Adds or modifies a single weighing identified by its date with data from the message body.";

        public const string DeleteName = "DeleteWeighing";

        public const string DeleteDesc = "Deletes a single weighing identified by its id.";

        public const int UserIdLength = 48;

        public const decimal MinWeight = 1;

        public const decimal MaxWeight = 1000;

        public const int DefaultLimit = 100;

        public static Error WeighingNotFound =
            Error.NotFound("Weighing.Date", "Weighing for the specified date was not found.");

        public static Error InvalidDateFormat =
            Error.Validation("Weighing.Date", "Weighing date string is an invalid format.");

        public static Error FutureDateError =
            Error.Validation("Weighing.Date", "Weighing date cannot be in the future.");

        public static Error WeightError =
            Error.Validation("Weighing.Weight", "Weight value must be between 1 and 1000.");
    }
}
