using HabitTracker.Web.Features.Categories;

namespace HabitTracker.Web.Features.Habits;

public partial class EditHabit
{
    public class ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public int TargetAttempts { get; set; } = 1;
    }

    private Error[] _errors = [];
    private Option<ViewModel> _vm = Option<ViewModel>.None();
    private CategoryResponse[] _categories = [];

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _http.TryGetFromJsonAsync<CategoryResponse[]>(Constants.Categories.ServiceUrl, [], _log)
                   .HandleResultAsync(c => _categories = c, e => _errors = e);

        await _http.TryGetByIdFromJsonAsync<HabitResponse>(Constants.Habits.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(h => _vm = CreateViewModel(h), e => _errors = e);
    }

    private async Task SaveHabit() =>
        await _http.TryPutAsJsonAsync<UpdateHabitRequest, HabitResponse>(
                        Constants.Habits.ServiceUrlWithId(Id),
                        CreateUpdateRequest(),
                        _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Habits.ListUrl), e => _errors = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Habits.ListUrl);

    private static ViewModel CreateViewModel(HabitResponse response) =>
        new()
        {
            Id = response.Id,
            Name = response.Name,
            Description = response.Description,
            CategoryId = response.Category.Id,
            TargetAttempts = response.TargetAttempts
        };

    private UpdateHabitRequest CreateUpdateRequest()
    {
        var edited = _vm.Get();
        return new(edited.Id, edited.Name, edited.Description, edited.CategoryId, edited.TargetAttempts);
    }
}
