namespace HabitTracker.Web.Common;

public class ProblemDetails
{
    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public int? Status { get; set; }

    public string Detail { get; set; } = string.Empty;

    public string Instance { get; set; } = string.Empty;

    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    public Error[] ToErrors()
    {
        return Errors.Select(key => Error.Create(
                                        key.Key,
                                        key.Value.FirstOrDefault() ?? string.Empty,
                                        Status ?? ErrorType.Unexpected))
                     .ToArray();
    }
}