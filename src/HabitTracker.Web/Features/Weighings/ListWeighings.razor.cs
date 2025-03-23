namespace HabitTracker.Web.Features.Weighings;

public partial class ListWeighings
{
    internal class ViewModel
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now.Date;

        public decimal Weight { get; set; } = 100;
    }

    private readonly ViewModel _vm = new();
    private List<WeighingResponse>? _weighings;
    private Error[] _errors = [];

    protected override async Task OnInitializedAsync() =>
        _weighings = await _http.TryGetFromJsonAsync<List<WeighingResponse>>(Constants.Weighings.ServiceUrl, [], _log)
                                .HandleErrorAsync(e => _errors = e, []);

    private async Task OnRecordWeight()
    {
        await _http.TryPutAsJsonAsync<UpsertWeighingRequest, WeighingResponse>(
                        Constants.Weighings.ServiceUrl,
                        new UpsertWeighingRequest(_vm.Date, _vm.Weight),
                        _log)
                   .HandleResultAsync(ReplaceLocalWeighing, e => _errors = e);
        StateHasChanged();
    }
    private async Task OnDeleteWeighing(int weighingId)
    {
        await _http.TryDeleteAsJsonAsync<WeighingResponse>(Constants.Weighings.ServiceUrlWithId(weighingId), _log)
                   .HandleResultAsync(s => _weighings?.Remove(s), e => _errors = e);
        StateHasChanged();
    }

    private void ReplaceLocalWeighing(WeighingResponse newWeighing)
    {
        var added = _weighings?.ReplaceFirstOrAdd(w => w.Date == newWeighing.Date, newWeighing);
        if (added is true)
            _weighings = _weighings?.OrderByDescending(w => w.Date).ToList() ?? [];

        _errors = [];
    }
}
