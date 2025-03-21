namespace HabitTracker.Web.Features.Habits;

public partial class ListHabits
{
    private HabitResponse[]? _habits;
    private Error[] _errors = [];

    protected override async Task OnInitializedAsync() =>
        _habits = await _http.TryGetFromJsonAsync<HabitResponse[]>(Constants.Habits.ServiceUrl, [], _log)
                             .HandleErrorAsync(e => _errors = e, []);
}
