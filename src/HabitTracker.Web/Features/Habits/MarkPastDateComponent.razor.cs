namespace HabitTracker.Web.Features.Habits;

public partial class MarkPastDateComponent
{
    private Option<string> displayMessage = Option<string>.None();
    private DateTimeOffset _date = DateTimeOffset.Now;

    [Parameter]
    public Option<HabitResponse> Habit { get; set; } = Option<HabitResponse>.None();

    private async Task OnMarkClicked(int habitId, DateTimeOffset date)
    {
        await _http.TryPutAsJsonAsync<MarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.MarkServiceUrl(habitId),
                        new(date.Date, 1),
                        _log)
                   .HandleResultAsync(
                        s => displayMessage = Constants.HabitCompletions.SuccessMarkIncremented(date),
                        e => displayMessage = e);
        StateHasChanged();
    }

    private async Task OnUnmarkClicked(int habitId, DateTimeOffset date)
    {
        await _http.TryPutAsJsonAsync<UnmarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.UnmarkServiceUrl(habitId),
                        new(date.Date, 1),
                        _log)
                   .HandleResultAsync(
                        s => displayMessage = Constants.HabitCompletions.SuccessUnmarkDecremented(date),
                        e => displayMessage = e);
        StateHasChanged();
    }
}
