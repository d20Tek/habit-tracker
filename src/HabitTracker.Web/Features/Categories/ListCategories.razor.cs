namespace HabitTracker.Web.Features.Categories;

public partial class ListCategories
{
    private CategoryResponse[]? _categories;
    private Error[] _errors = [];

    protected override async Task OnInitializedAsync() =>
        _categories = await _http.TryGetFromJsonAsync<CategoryResponse[]>(Constants.Categories.ServiceUrl, [], _log)
                                 .HandleErrorAsync(e => _errors = e, []);
}
