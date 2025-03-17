namespace HabitTracker.Web.Features.Habits;

public partial class DetailHabit
{
    private Option<string> _errorMessage = Option<string>.None();
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _http.TryGetByIdFromJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(h => _habit = h, e => _errorMessage = e);
    }
}
