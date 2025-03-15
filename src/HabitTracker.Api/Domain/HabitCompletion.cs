namespace HabitTracker.Api.Domain;

internal class HabitCompletion
{
    public int Id { get; set; }

    public DateTimeOffset CompletionDate { get; set; }
    
    public int CompletionCount { get; set; }

    public static HabitCompletion Create(DateTimeOffset date, int count) =>
        new() { CompletionDate = date, CompletionCount = count };

    public void Increment(int amount) => CompletionCount += amount;

    public void Decrement(int amount) => CompletionCount -= amount;
}
