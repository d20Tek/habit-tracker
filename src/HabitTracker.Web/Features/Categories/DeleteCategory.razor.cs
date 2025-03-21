namespace HabitTracker.Web.Features.Categories;

public partial class DeleteCategory
{
    private Error[] _errors = [];
    private Option<CategoryResponse> _category = Option<CategoryResponse>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync() =>
        _category = await _http.TryGetByIdFromJsonAsync<CategoryResponse>(
                                    Constants.Categories.ServiceUrlWithId(Id), _log)
                               .HandleErrorAsync(e => _errors = e, default!);

    private async Task DeleteHandler() =>
        await _http.TryDeleteAsJsonAsync<CategoryResponse>(Constants.Categories.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Categories.ListUrl), e => _errors = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Categories.ListUrl);
}
