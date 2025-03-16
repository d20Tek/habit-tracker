namespace HabitTracker.Web.Features.Habits;

public partial class MarkHabitsComponent
{
    private HabitResponse[]? _habits;
    private Option<string> _errorMessage = Option<string>.None();

    [Parameter]
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    protected override async Task OnInitializedAsync() =>
        _habits = await _http.TryGetFromJsonAsync<HabitResponse[]>(Constants.Habits.ServiceUrl, [], _log)
                             .HandleErrorAsync(e => _errorMessage = e, []);

    private async Task OnMarkClicked(int habitId)
    {
        await _http.TryPutAsJsonAsync<MarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.MarkServiceUrl(habitId),
                        new(DateTimeOffset.Now.Date, 1),
                        _log)
                   .HandleResultAsync(s => ReplaceLocalHabit(s), e => _errorMessage = e);
    }

    private async Task OnUnmarkClicked(int habitId)
    {
        await _http.TryPutAsJsonAsync<UnmarkHabitRequest, HabitResponse>(
                        Constants.HabitCompletions.UnmarkServiceUrl(habitId),
                        new(DateTimeOffset.Now.Date, 1),
                        _log)
                   .HandleResultAsync(s => ReplaceLocalHabit(s), e => _errorMessage = e);
    }

    private void ReplaceLocalHabit(HabitResponse newHabit)
    {
        if (_habits is null) return;

        int index = Array.FindIndex(_habits, h => h.Id == newHabit.Id);
        if (index >= 0)
        {
            _habits[index] = newHabit;
        }

        _errorMessage = Option<string>.None();
        StateHasChanged();
    }
}
