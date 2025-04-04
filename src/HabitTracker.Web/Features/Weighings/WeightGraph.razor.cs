namespace HabitTracker.Web.Features.Weighings;

public partial class WeightGraph
{
    private IEnumerable<WeighingResponse> _weighings = [];
    private decimal _maxWeight = Constants.Weighings.MaxWeight;
    private decimal _minWeight = Constants.Weighings.MinWeight;
    private decimal[] _yAxisLabels = [];
    private Option<WeighingResponse> _selectedWeighing = Option<WeighingResponse>.None();

    [Parameter]
    public List<WeighingResponse> Weighings { get; set; } = [];

    protected override void OnParametersSet()
    {
        _weighings = Weighings.Take(Constants.Weighings.WeightGraphMaxColumns).Reverse();
        if (_weighings.Any())
        {
            _maxWeight = GetMaxWeight(_weighings);
            _minWeight = CalculateMinWeight(_weighings, _maxWeight, CalculateDelta);
            _yAxisLabels = CreateAxisLabels(_minWeight, CalculateDelta(_minWeight, _maxWeight));
        }
    }

    private static decimal GetMaxWeight(IEnumerable<WeighingResponse> w) =>  w.MaxBy(x => x.Weight)!.Weight;

    private static decimal CalculateMinWeight(
        IEnumerable<WeighingResponse> weighings,
        decimal maxWeight,
        Func<decimal, decimal, decimal> deltaFunc)
    {
        decimal minWeight = weighings.MinBy(x => x.Weight)!.Weight;
        decimal delta = deltaFunc(minWeight, maxWeight);

        return minWeight - (delta / Constants.Weighings.DeltaFactor);
    }

    private static decimal[] CreateAxisLabels(decimal minWeight, decimal delta) =>
        [..Enumerable.Range(0, Constants.Weighings.GraphAxisLabels)
                     .Select(i => Math.Round(minWeight + (i * (delta / Constants.Weighings.GraphAxisFactor)), 1))
                     .Reverse()];

    private static decimal CalculateDelta(decimal minWeight, decimal maxWeight) =>
        Math.Max(maxWeight - minWeight, 1);

    private void OnBarClicked(WeighingResponse weighing) => _selectedWeighing = weighing;

    private string GetBarCss(WeighingResponse weighing) => 
        _selectedWeighing.Match(
            s => s.Id == weighing.Id ? Constants.Weighings.SelectedBarCss : Constants.Weighings.NormalBarCss,
            () => Constants.Weighings.NormalBarCss);
}
