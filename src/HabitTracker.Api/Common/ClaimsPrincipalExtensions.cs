namespace HabitTracker.Api.Common;

internal static class ClaimsPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal user) =>
        user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}
