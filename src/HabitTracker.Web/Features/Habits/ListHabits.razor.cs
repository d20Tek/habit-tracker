namespace HabitTracker.Web.Features.Habits;

public partial class ListHabits
{
    private HabitResponse[]? _habits;
    private Option<string> _errorMessage = Option<string>.None();

    protected override async Task OnInitializedAsync() =>
        _habits = await _http.TryGetFromJsonAsync<HabitResponse[]>(Constants.Habits.ServiceUrl, [], _log)
                             .HandleErrorAsync(e => _errorMessage = e, []);
}
