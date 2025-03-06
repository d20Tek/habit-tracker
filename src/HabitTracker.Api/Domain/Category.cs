namespace HabitTracker.Api.Domain;

internal class Category
{
    public int CategoryId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}