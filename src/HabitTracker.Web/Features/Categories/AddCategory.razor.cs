namespace HabitTracker.Web.Features.Categories;

public partial class AddCategory
{
    public class ViewModel
    {
        public string Name { get; set; } = string.Empty;
    }

    private Error[] _errors = [];
    private readonly ViewModel _vm = new();

    private async Task CreateCategory() =>
        await _http.TryPostAsJsonAsync<CreateCategoryRequest, CategoryResponse>(
                        Constants.Categories.ServiceUrl,
                        new CreateCategoryRequest(_vm.Name),
                        _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Categories.ListUrl), e => _errors = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Categories.ListUrl);
}
