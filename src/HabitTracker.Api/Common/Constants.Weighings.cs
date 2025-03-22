namespace HabitTracker.Api.Common;

internal partial class Constants
{
    internal static class Weighings
    {
        public const string ServiceBase = "/api/v1/weighing";

        public const string ServiceBaseWithId = ServiceBase + "/{id:int}";

        public const string GetAllName = "GetAllWeighings";

        public const string GetAllDesc = "Retrieves a list of weighings for the user.";

        public const string GetByDateName = "GetWeighingByDate";

        public const string GetByDateDesc = "Retrieves a single weighing identified by its date.";

        public const string UpsertName = "UpsertWeighing";

        public const string UpsertDesc =
            "Adds or modifies a single weighing identified by its date with data from the message body.";

        public const string DeleteName = "DeleteWeighing";

        public const string DeleteDesc = "Deletes a single weighing identified by its date.";

        public const int UserIdLength = 48;
        public const decimal MinWeight = 1;
        public const decimal MaxWeight = 1000;
    }
}
