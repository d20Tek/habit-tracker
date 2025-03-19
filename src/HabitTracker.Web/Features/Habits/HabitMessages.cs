using HabitTracker.Web.Features.Categories;

namespace HabitTracker.Web.Features.Habits;

public record HabitResponse(
    int Id,
    string Name,
    string? Description,
    string UserId,
    CategoryResponse Category,
    int TargetAttempts,
    CompletionResponse[] Completions)
{
    public int GetCompletionCount(DateTimeOffset date)
    {
        var completion = Completions.SingleOrDefault(c => c.Date == date.Date);
        return (completion is not null) ? completion.Count : 0;
    }

    public bool IsCompleted(DateTimeOffset date)
    {
        var completion = Completions.SingleOrDefault(c => c.Date == date.Date);
        return completion is not null && completion.Count >= TargetAttempts;
    }

    public string ToCompletionString(DateTimeOffset date) =>
        $"{GetCompletionCount(date.Date)} / {TargetAttempts}";
}

public record CompletionResponse(int Id, DateTimeOffset Date, int Count);

internal record CreateHabitRequest(
    string Name,
    string? Description,
    int CategoryId,
    int TargetAttempts);

internal record UpdateHabitRequest(
    int Id,
    string Name,
    string? Description,
    int CategoryId,
    int TargetAttempts);

internal record MarkHabitRequest(DateTimeOffset Date, int Increment = 1);

internal record UnmarkHabitRequest(DateTimeOffset Date, int Decrement = 1);
