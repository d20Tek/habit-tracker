namespace HabitTracker.Web.Features.Habits;

public partial class DeleteHabit
{
    private Option<string> _errorMessage = Option<string>.None();
    private Option<HabitResponse> _habit = Option<HabitResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync() =>
        _habit = await _http.TryGetByIdFromJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                            .HandleErrorAsync(e => _errorMessage = e, default!);

    private async Task DeleteHandler() =>
        await _http.TryDeleteAsJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Habits.ListUrl), e => _errorMessage = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Habits.ListUrl);
}
