namespace HabitTracker.Web.Features.Habits;

public partial class DeleteHabit
{
    private Error[] _errors = [];
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync() =>
        _habit = await _http.TryGetByIdFromJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                            .HandleErrorAsync(e => _errors = e, default!);

    private async Task DeleteHandler() =>
        await _http.TryDeleteAsJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Habits.ListUrl), e => _errors = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Habits.ListUrl);
}
