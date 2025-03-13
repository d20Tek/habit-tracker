namespace HabitTracker.Web.Features.Categories;

public partial class EditCategory
{
    public class ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    private Option<string> _errorMessage = Option<string>.None();
    private Option<ViewModel> _vm = Option<ViewModel>.None();

    [Parameter]
    public int Id { get; set; }

    protected override async Task OnInitializedAsync() =>
        await _http.TryGetByIdFromJsonAsync<CategoryResponse>(Constants.Categories.ServiceUrlWithId(Id), _log)
                   .HandleResultAsync(c => _vm = CreateViewModel(c), e => _errorMessage = e);

    private async Task SaveCategory() =>
        await _http.TryPutAsJsonAsync<UpdateCategoryRequest, CategoryResponse>(
                        Constants.Categories.ServiceUrlWithId(Id),
                        CreateUpdateRequest(),
                        _log)
                   .HandleResultAsync(s => _nav.NavigateTo(Constants.Categories.ListUrl), e => _errorMessage = e);

    private void CancelHandler() => _nav.NavigateTo(Constants.Categories.ListUrl);

    private static ViewModel CreateViewModel(CategoryResponse response) =>
        new() { Id = response.Id, Name = response.Name };

    private UpdateCategoryRequest CreateUpdateRequest() => 
        _vm.Match(vm => new UpdateCategoryRequest(vm.Id, vm.Name),
                  () =>
                  {
                      _errorMessage = "Unexpected error... UpdateCategory request could not be created.";
                      return new UpdateCategoryRequest(0, string.Empty);
                  });
}
