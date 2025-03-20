namespace HabitTracker.Web.Features.Habits.Components;

public class CalendarDay
{
    public DateTimeOffset? Date { get; init; }

    public HabitStatus Status { get; init; } = HabitStatus.Empty;

    public string? Color { get; init; }

    public string? CompletionDisplay { get; init; }

    private CalendarDay(DateTimeOffset date, HabitStatus status, string display)
    {
        Date = date;
        Status = status;
        Color = GetStatusColor(status);
        CompletionDisplay = display;
    }

    private CalendarDay() { }

    public static CalendarDay Empty => new();

    public static CalendarDay Create(DateTimeOffset date, HabitResponse habit) => new(
        date,
        CalculateHabitStatus(habit.GetCompletionCount(date), habit.TargetAttempts),
        Constants.HabitStatus.CompletionDisplay(habit.ToCompletionString(date), date));

    private static HabitStatus CalculateHabitStatus(int completions, int target) =>
        completions switch
        {
            0 => HabitStatus.NotStarted,
            var c when c < target => HabitStatus.InProgress,
            var c when c == target => HabitStatus.Completed,
            _ => HabitStatus.OverAchieved
        };

    private static string GetStatusColor(HabitStatus status) =>
        status switch
        {
            HabitStatus.NotStarted => Constants.HabitStatus.NotStartedColor,
            HabitStatus.InProgress => Constants.HabitStatus.InProgressColor,
            HabitStatus.Completed => Constants.HabitStatus.CompletedColor,
            HabitStatus.OverAchieved => Constants.HabitStatus.OverAchievedColor,
            _ => Constants.HabitStatus.EmptyColor
        };
}
