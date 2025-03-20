namespace HabitTracker.Web.Features.Habits;

internal class GridPosition
{
    public int Row { get; private set; }

    public int Col { get; private set; }

    public GridPosition(DayOfWeek day)
    {
        Row = CalculateStartingRow(day);
        Col = (int)day;
    }

    public void Decrement()
    {
        if (Col == 0)
        {
            Col = Constants.HabitMonth.Columns - 1;
            Row--;
        }
        else
        {
            Col--;
        }
    }

    private static int CalculateStartingRow(DayOfWeek day) =>
        day < DayOfWeek.Tuesday ? Constants.HabitMonth.StartRowFull : Constants.HabitMonth.StartRowShort;
}
