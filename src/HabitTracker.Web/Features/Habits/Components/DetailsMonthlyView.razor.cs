namespace HabitTracker.Web.Features.Habits.Components;

public partial class DetailsMonthlyView
{
    private Error[] _errors = [];
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int HabitId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var fullUrl = Constants.Habits.ServiceUrlWithLimit(HabitId, Constants.Habits.LimitMonthly);
        await _http.TryGetByIdFromJsonAsync<HabitResponse>(fullUrl, _log)
                   .HandleResultAsync(h => _habit = h, e => _errors = e);
    }
}
