namespace HabitTracker.Web.Features.Weighings;

public record WeighingResponse(int Id, DateTimeOffset Date, decimal Weight)
{
    public decimal WeightPct(decimal minWeight, decimal maxWeight)
    {
        var weightRange = Math.Max(maxWeight - minWeight, 1);
        var normalizedHeight = (Weight - minWeight) / weightRange;
        return Math.Max(normalizedHeight * Constants.Weighings.Percentage, 0);
    }

    public string DisplayText() => $"{Date:MMM dd, yyyy} - {Weight:0.0}";
}

internal record UpsertWeighingRequest(DateTimeOffset Date, decimal Weight);
