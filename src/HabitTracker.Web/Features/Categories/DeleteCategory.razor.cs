namespace HabitTracker.Web.Features.Categories;

public partial class DeleteCategory
{
    private Option<string> _errorMessage = Option<string>.None();
    private Option<CategoryResponse> _category = Option<CategoryResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync() =>
        _category = await _http.TryGetByIdFromJsonAsync<CategoryResponse>(
                                    Constants.Categories.ServiceUrlWithId(Id), _log)
                               .HandleErrorAsync(e => _errorMessage = e, default!);

    private async Task DeleteHandler() =>
        await _http.TryDeleteAsJsonAsync<CategoryResponse>(Constants.Categories.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Categories.ListUrl), e => _errorMessage = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Categories.ListUrl);
}
