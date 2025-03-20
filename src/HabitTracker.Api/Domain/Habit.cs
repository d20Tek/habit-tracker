namespace HabitTracker.Api.Domain;

internal class Habit
{
    public int HabitId { get; private set; }

    public string UserId { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public int CategoryId { get; private set; }

    public Category? Category { get; private set; }

    public int TargetAttempts { get; private set; }

    public List<HabitCompletion> DailyCompletions { get; private set; } = [];

    public static Habit Create(string name, string? desc, string userId, int categoryId, int targetAttempts = 1)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(targetAttempts);

        return new()
        {
            Name = name,
            Description = desc,
            UserId = userId,
            CategoryId = categoryId,
            TargetAttempts = targetAttempts
        };
    }

    public void UpdateHabitInfo(string name, string? desc, int categoryId, int targetAttempts)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(targetAttempts);

        Name = name;
        Description = desc;
        CategoryId = categoryId;
        TargetAttempts = targetAttempts;
    }

    internal void MarkCompleted(DateTimeOffset date, int incrementAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(incrementAmount);

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

    internal void UnmarkCompleted(DateTimeOffset date, int decrementAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(decrementAmount);

        var completion = DailyCompletions.SingleOrDefault(c => c.CompletionDate.Date == date);
        if (completion is not null)
        {
            if (decrementAmount > completion.CompletionCount)
            {
                DailyCompletions.Remove(completion);
            }
            else
            {
                completion.Decrement(decrementAmount);
            }
        }
    }
}
