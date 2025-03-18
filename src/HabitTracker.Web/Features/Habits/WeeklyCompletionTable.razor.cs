namespace HabitTracker.Web.Features.Habits;

public partial class WeeklyCompletionTable
{
    private DateTimeOffset[] _dateRange = [];
    private Option<string> _errorMessage = Option<string>.None();

    [Parameter]
    public Option<HabitResponse> Habit { get; set; } = Option<HabitResponse>.None();

    protected override void OnParametersSet() =>
        _dateRange = DateRangeFactory.GetDateRangeForWeek();

    private async Task OnMarkClicked(int habitId, DateTimeOffset date)
    {
        await _http.TryPutAsJsonAsync<MarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.MarkServiceUrl(habitId, Constants.Habits.LimitWeekly),
                        new(date.Date, 1),
                        _log)
                   .HandleResultAsync(s => Habit = s, e => _errorMessage = e);
        StateHasChanged();
    }

    private async Task OnUnmarkClicked(int habitId, DateTimeOffset date)
    {
        await _http.TryPutAsJsonAsync<UnmarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.UnmarkServiceUrl(habitId, Constants.Habits.LimitWeekly),
                        new(date.Date, 1),
                        _log)
                   .HandleResultAsync(s => Habit = s, e => _errorMessage = e);
        StateHasChanged();
    }
}
