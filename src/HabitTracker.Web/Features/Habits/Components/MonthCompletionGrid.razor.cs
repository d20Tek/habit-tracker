namespace HabitTracker.Web.Features.Habits.Components;

public partial class MonthCompletionGrid
{
    private readonly CalendarDay[,] _calendarGrid;

    [Parameter]
    public Option<HabitResponse> Habit { get; set; } = Option<HabitResponse>.None();

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    public MonthCompletionGrid()
    {
        _calendarGrid = new CalendarDay[Constants.HabitMonth.Rows, Constants.HabitMonth.Columns];

        for (var r = 0; r < Constants.HabitMonth.Rows; r++)
        {
            for (var c = 0; c < Constants.HabitMonth.Columns; c++)
            {
                _calendarGrid[r, c] = CalendarDay.Empty;
            }
        }
    }

    protected override void OnInitialized() => FillCalendarGrid(DateTime.Today);

    private void FillCalendarGrid(DateTimeOffset today) =>
        FillCalendarGrid(DateRangeFactory.GetDateRangeForMonth(today), new GridPosition(today.DayOfWeek));

    private void FillCalendarGrid(DateTimeOffset[] dates, GridPosition pos)
    {
        for (var i = dates.Length - 1; i >= 0; i--)
        {
            _calendarGrid[pos.Row, pos.Col] = CalendarDay.Create(dates[i], Habit.Get());
            pos.Decrement();
        }
    }
}
