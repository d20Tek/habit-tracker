using HabitTracker.Web.Features.Habits;

namespace HabitTracker.Web.Features.Reports;

public partial class MonthlyReport
{
    private HabitResponse[]? _habits;
    private Option<string> _errorMessage = Option<string>.None();

    protected override async Task OnInitializedAsync()
    {
        var fullUrl = Constants.Habits.ServiceUrlWithLimit(Constants.Habits.LimitMonthly);
        await _http.TryGetFromJsonAsync<HabitResponse[]>(fullUrl, [], _log)
                   .HandleResultAsync(h => _habits = h, e => _errorMessage = e);
    }
}
