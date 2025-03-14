using HabitTracker.Web.Features.Categories;

namespace HabitTracker.Web.Features.Habits;

internal record HabitResponse(
    int Id,
    string Name,
    string? Description,
    string UserId,
    CategoryResponse Category,
    int TargetAttempts,
    CompletionResponse[] Completions);

internal record CompletionResponse(int Id, DateTimeOffset Date, int Count);

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
