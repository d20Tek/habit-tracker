namespace HabitTracker.Web.Common;

internal static class DateRangeFactory
{
    public static DateTimeOffset[] GetDateRangeForWeek() => GetDateRangeForWeek(DateTimeOffset.Now);

    public static DateTimeOffset[] GetDateRangeForWeek(DateTimeOffset weekEndDate)
    {
        DateTimeOffset[] dates = new DateTimeOffset[Constants.Habits.LimitWeekly];
        var decReverse = -Constants.Habits.LimitWeekly + 1;

        for (int i = 0; i < dates.Length; i++)
        {
            dates[i] = weekEndDate.AddDays(decReverse + i);
        }

        return dates;
    }
}
