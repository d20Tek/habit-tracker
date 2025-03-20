namespace HabitTracker.Web.Features.Habits;

public partial class DetailsMonthlyView
{
    private Option<string> _errorMessage = Option<string>.None();
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int HabitId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var fullUrl = Constants.Habits.ServiceUrlWithLimit(HabitId, Constants.Habits.LimitMonthly);
        await _http.TryGetByIdFromJsonAsync<HabitResponse>(fullUrl, _log)
                   .HandleResultAsync(h => _habit = h, e => _errorMessage = e);
    }
}
