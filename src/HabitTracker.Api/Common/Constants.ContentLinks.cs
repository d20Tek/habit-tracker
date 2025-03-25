namespace HabitTracker.Api.Common;

internal static partial class Constants
{
    internal static class ContentLinks
    {
        public const string ServiceBase = "/api/v1/content-links/{group}";

        public const string GetAllName = "GetContentLinks";

        public const string GetAllDesc = "Retrieves a list of ccontent links for a particular grouping.";

        public static string GetCacheKey(string group) => $"content_links_{group}";

        public static TimeSpan CacheExpiration = TimeSpan.FromHours(1);

        public const int TitleLength = 100;

        public const int DescLength = 250;

        public const int UrlLength = 500;

        public const int GroupLength = 100;

        public const int GroupLinkLimit = 5;

        public static Error RequiredGroupError = Error.Validation("ContentLink.Group", "Content link is a required.");
    }
}
