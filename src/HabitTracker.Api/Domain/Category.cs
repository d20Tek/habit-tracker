namespace HabitTracker.Api.Domain;

internal class Category
{
    public int CategoryId { get; private set; }

    public string UserId { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    private Category(int categoryId, string name, string userId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(categoryId);
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNullOrEmpty(userId);

        CategoryId = categoryId;
        UserId = userId;
        Name = name;
    }

    public static Category Create(int id, string name, string userId) => new(id, name, userId);

    public static Category Create(string name, string userId) => new(0, name, userId);

    public Category Rename(string name)
    {
        Name = name;
        return this;
    }
}
