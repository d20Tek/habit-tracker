namespace HabitTracker.Api.Domain;

internal class ContentLink
{
    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Url { get; private set; } = string.Empty;

    public int SortOrder { get; private set; }

    public string Group { get; private set; } = string.Empty;

    public static ContentLink Create(string title, string? desc, string url, int sortOrder, string group) =>
        new()
        {
            Title = title,
            Description = desc,
            Url = url,
            SortOrder = sortOrder,
            Group = group
        };

    public static ContentLink Create(int id, string title, string? desc, string url, int sortOrder, string group) =>
        new()
        {
            Id = id,
            Title = title,
            Description = desc,
            Url = url,
            SortOrder = sortOrder,
            Group = group
        };
}
