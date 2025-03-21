namespace HabitTracker.Api.Domain;

public class Weighing
{
    public int WeighingId { get; private set; }

    public string UserId { get; private set; } = string.Empty;

    public DateTimeOffset Date { get; private set; }

    public decimal Weight { get; private set; }

    public static Weighing Create(string userId, DateTimeOffset date, decimal weight) =>
        new() { UserId = userId, Date = date, Weight = weight };

    public void ChangeWeight(decimal weight) => Weight = weight;
}
