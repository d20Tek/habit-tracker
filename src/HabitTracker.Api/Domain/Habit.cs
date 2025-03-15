namespace HabitTracker.Api.Domain;

internal class Habit
{
    public int HabitId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get;  set; }

    public int TargetAttempts { get; set; }

    public List<HabitCompletion> DailyCompletions { get; set; } = [];

    public static Habit Create(string name, string? desc, string userId, int categoryId, int targetAttempts = 1) =>
        new()
        {
            Name = name,
            Description = desc,
            UserId = userId,
            CategoryId = categoryId,
            TargetAttempts = targetAttempts
        };

    internal void MarkCompleted(DateTimeOffset date, int incrementAmount)
    {
        var completion = DailyCompletions.SingleOrDefault(c => c.CompletionDate.Date == date);
        if (completion is null)
        {
            DailyCompletions.Add(HabitCompletion.Create(date.Date, incrementAmount));
        }
        else
        {
            completion.Increment(incrementAmount);
        }
    }

    internal void UnmarkCompleted(DateTimeOffset date, int decrement)
    {
        var completion = DailyCompletions.SingleOrDefault(c => c.CompletionDate.Date == date);
        if (completion is not null)
        {
            if (decrement > completion.CompletionCount)
            {
                DailyCompletions.Remove(completion);
            }
            else
            {
                completion.Decrement(decrement);
            }
        }
    }
}
