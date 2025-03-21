using HabitTracker.Web.Features.Categories;

namespace HabitTracker.Web.Features.Habits;

public partial class AddHabit
{
    internal class ViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public int TargetAttempts { get; set; } = 1;
    }

    private Error[] _errors = [];
    private CategoryResponse[] _categories = [];
    private readonly ViewModel _vm = new();

    protected override async Task OnInitializedAsync() =>
        await _http.TryGetFromJsonAsync<CategoryResponse[]>(Constants.Categories.ServiceUrl, [], _log)
                   .HandleResultAsync(c =>
                   {
                       _categories = c;
                       _vm.CategoryId = c.FirstOrDefault()?.Id ?? 0;
                   }
                   , e => _errors = e);

    private async Task CreateHabit() =>
        await _http.TryPostAsJsonAsync<CreateHabitRequest, HabitResponse>(
                        Constants.Habits.ServiceUrl, CreateRequest(), _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Habits.ListUrl), e => _errors = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Habits.ListUrl);

    private CreateHabitRequest CreateRequest() =>
        new (_vm.Name, _vm.Description, _vm.CategoryId, _vm.TargetAttempts);
}
