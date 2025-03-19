using HabitTracker.Web.Features.Habits;

namespace HabitTracker.Web.Features.Reports;

public partial class WeeklyReport
{
    private DateTimeOffset[] _dateRange = [];
    private HabitResponse[]? _habits;
    private Option<string> _errorMessage = Option<string>.None();

    protected override async Task OnInitializedAsync()
    {
        _dateRange = DateRangeFactory.GetDateRangeForWeek();

        var fullUrl = Constants.Habits.ServiceUrlWithLimit(Constants.Habits.LimitWeekly);
        await _http.TryGetFromJsonAsync<HabitResponse[]>(fullUrl, [], _log)
                   .HandleResultAsync(h => _habits = h, e => _errorMessage = e);
    }
}
