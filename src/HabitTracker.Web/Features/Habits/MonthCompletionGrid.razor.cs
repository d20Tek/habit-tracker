namespace HabitTracker.Web.Features.Habits;

public partial class MonthCompletionGrid
{
    private const int _rows = 6;
    private const int _columns = 7;

    [Parameter]
    public Option<HabitResponse> Habit { get; set; } = Option<HabitResponse>.None();

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private readonly CalendarDay[,] _calendarGrid = new CalendarDay[_rows, _columns];

    public MonthCompletionGrid()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                _calendarGrid[r, c] = CalendarDay.Empty;
            }
        }
    }

    protected override void OnInitialized()
    {
        FillCalendarGrid();
    }

    private void FillCalendarGrid()
    {
        var today = DateTime.Today;
        var dates = DateRangeFactory.GetDateRangeForMonth(today);

        int currentCol = (int)today.DayOfWeek;
        int currentRow = CalculateStartingRow(currentCol);

        for (int i = dates.Length - 1; i >= 0; i--)
        {
            _calendarGrid[currentRow, currentCol] = CalendarDay.Create(dates[i], Habit.Get());

            if (currentCol == 0)
            {
                currentCol = 6;
                currentRow--;
            }
            else
            {
                currentCol--;
            }
        }
    }

    private int CalculateStartingRow(int col) => col < 2 ? 5 : 4;
}
