namespace HabitTracker.Api.Domain;

internal class HabitCompletion
{
    public int Id { get; set; }

    public DateTimeOffset CompletionDate { get; set; }
    
    public int CompletionCount { get; set; }
}
