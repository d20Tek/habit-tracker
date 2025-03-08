using D20Tek.Functional;
using HabitTracker.Web.Common;

namespace HabitTracker.Web.Features.Categories;

public partial class AddCategory
{
    public class ViewModel
    {
        public string Name { get; set; } = string.Empty;
    }

    private Option<string> _errorMessage = Option<string>.None();
    private readonly ViewModel _vm = new();

    private async Task CreateCategory() =>
        await _http.TryPostAsJsonAsync<CreateCategoryRequest, CategoryResponse>(
                        Constants.Categories.ServiceUrl,
                        new CreateCategoryRequest(_vm.Name))
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Categories.ListUrl), e => _errorMessage = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Categories.ListUrl);
}
