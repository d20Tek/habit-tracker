namespace HabitTracker.Api.Domain;

internal class HabitCompletion
{
    public int Id { get; private set; }

    public DateTimeOffset CompletionDate { get; private set; }

    public int CompletionCount { get; private set; }

    public static HabitCompletion Create(DateTimeOffset date, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return new() { CompletionDate = date, CompletionCount = count };
    }

    public void Increment(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        CompletionCount += amount;
    }

    public void Decrement(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        CompletionCount -= amount;
    }
}
