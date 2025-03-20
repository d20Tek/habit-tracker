namespace HabitTracker.Web.Features.Habits.Components;

public partial class MarkHabitsComponent
{
    private HabitResponse[]? _habits;
    private Option<string> _errorMessage = Option<string>.None();

    [Parameter]
    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    [Parameter]
    public bool ShowHeader { get; set; } = true;

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
        _habits?.ReplaceFirst(h => h.Id == newHabit.Id, newHabit);
        _errorMessage = Option<string>.None();
        StateHasChanged();
    }
}
