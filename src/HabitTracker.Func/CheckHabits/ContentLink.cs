namespace HabitTracker.Func.CheckHabits;

public class ContentLink
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Url { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public string Group { get; set; } = string.Empty;
}
