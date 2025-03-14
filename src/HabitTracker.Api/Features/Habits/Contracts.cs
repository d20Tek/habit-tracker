using HabitTracker.Api.Features.Categories;
using System.Text.Json.Serialization;

namespace HabitTracker.Api.Features.Habits;

internal record HabitResponse(
    int Id,
    string Name,
    string? Description,
    string UserId,
    CategoryResponse Category,
    int TargetAttempts,
    CompletionResponse[] Completions)
{
    public static HabitResponse FromEntity(Habit habit) =>
        new(
            habit.HabitId,
            habit.Name,
            habit.Description,
            habit.UserId,
            CategoryResponse.FromEntity(habit.Category!),
            habit.TargetAttempts,
            [.. habit.DailyCompletions.Select(c => CompletionResponse.FromEntity(c))]);
}

internal record CompletionResponse(int Id, DateTimeOffset Date, int Count)
{
    public static CompletionResponse FromEntity(HabitCompletion hc) =>
        new(hc.Id, hc.CompletionDate, hc.CompletionCount);
}

internal record CreateHabitRequest(string Name, string? Description, int CategoryId, int TargetAttempts)
{
    [JsonIgnore]
    public string UserId { get; private set; } = string.Empty;

    public CreateHabitRequest AppendUserId(string userId) => this with { UserId = userId };

    public Habit ToEntity() => Habit.Create(
        Name,
        Description,
        UserId,
        CategoryId,
        TargetAttempts);
}

internal record UpdateHabitRequest(
    int Id,
    string Name,
    string? Description,
    int CategoryId,
    int TargetAttempts)
{
    [JsonIgnore]
    public string UserId { get; private set; } = string.Empty;

    public UpdateHabitRequest AppendUserId(string userId) => this with { UserId = userId };
}

internal record DeleteHabitRequest(int Id, string UserId);

internal record GetHabitByIdRequest(int Id, string UserId);
