namespace HabitTracker.Web.Features.Habits;

public partial class DetailHabit
{
    private Error[] _errors = [];
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();
    private ViewType SelectedView = ViewType.Weekly;

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var fullUrl = Constants.Habits.ServiceUrlWithLimit(Id, Constants.Habits.LimitWeekly);
        await _http.TryGetByIdFromJsonAsync<HabitResponse>(fullUrl, _log)
                   .HandleResultAsync(h => _habit = h, e => _errors = e);
    }

    private string GetButtonCss(ViewType id) => SelectedView == id ? Constants.Habits.ActiveButton : string.Empty;

    private void ChangeView(ViewType id) => SelectedView = id;

    internal enum ViewType { Weekly = 1, Monthly =  2, PastDate = 3 }
}
