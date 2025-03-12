using D20Tek.Functional;

namespace HabitTracker.Api.Common;

internal static class ResultLogExtensions
{
    public static string LogDetails<T>(this Result<T> result) where T: notnull =>
        result.Match(s => Constants.EndpointSuccess, e => string.Join(", ", e));
}
