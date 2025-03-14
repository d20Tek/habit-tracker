namespace HabitTracker.Web.Features.Categories;

public partial class ListCategories
{
    private CategoryResponse[]? _categories;
    private Option<string> _errorMessage = Option<string>.None();

    protected override async Task OnInitializedAsync() =>
        _categories = await _http.TryGetFromJsonAsync<CategoryResponse[]>(Constants.Categories.ServiceUrl, [], _log)
                                 .HandleErrorAsync(e => _errorMessage = e, []);
}
