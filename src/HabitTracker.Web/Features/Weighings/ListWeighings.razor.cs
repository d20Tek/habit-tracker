namespace HabitTracker.Web.Features.Weighings;

public partial class ListWeighings
{
    internal class ViewModel
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now.Date;

        public decimal Weight { get; set; } = 100;
    }

    private ViewModel _vm = new();
    private List<WeighingResponse>? _weighings;
    private Error[] _errors = [];

    protected override async Task OnInitializedAsync() =>
        _weighings = await _http.TryGetFromJsonAsync<List<WeighingResponse>>(Constants.Weighings.ServiceUrl, [], _log)
                                .HandleErrorAsync(e => SetErrors(e), []);

    private async Task OnRecordWeight() => 
        await _http.TryPutAsJsonAsync<UpsertWeighingRequest, WeighingResponse>(
                        Constants.Weighings.ServiceUrl,
                        new UpsertWeighingRequest(_vm.Date, _vm.Weight),
                        _log)
                   .HandleResultAsync(ReplaceLocalWeighing, e => SetErrors(e));

    private async Task OnDeleteWeighing(DateTimeOffset date) =>
        await _http.TryDeleteAsJsonAsync<WeighingResponse>(Constants.Weighings.ServiceUrlWithDate(date.Date), _log)
                   .HandleResultAsync(s => _weighings?.Remove(s), e => SetErrors(e));

    private void ReplaceLocalWeighing(WeighingResponse newWeighing)
    {
        _weighings?.ReplaceFirstOrAdd(w => w.Date == newWeighing.Date, newWeighing);
        _errors = [];
        StateHasChanged();
    }

    private void SetErrors(Error[] errors)
    {
        _errors = errors;
        StateHasChanged() ;
    }
}
