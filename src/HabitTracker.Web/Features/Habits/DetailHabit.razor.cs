namespace HabitTracker.Web.Features.Habits;

public partial class DetailHabit
{
    private Option<string> _errorMessage = Option<string>.None();
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var fullUrl = Constants.Habits.ServiceUrlWithLimit(Id, Constants.Habits.LimitWeekly);
        await _http.TryGetByIdFromJsonAsync<HabitResponse>(fullUrl, _log)
                   .HandleResultAsync(h => _habit = h, e => _errorMessage = e);
    }
}
