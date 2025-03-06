namespace HabitTracker.Api.Domain;

internal class HabitCompletion
{
    public int Id { get; set; }

    public DateTimeOffset CompletionDate { get; private set; }
    
    public int CompletionCount { get; private set; }
}
