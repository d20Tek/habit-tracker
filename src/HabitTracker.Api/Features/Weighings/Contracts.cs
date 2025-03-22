using System.Text.Json.Serialization;

namespace HabitTracker.Api.Features.Weighings;

internal record WeighingResponse(int Id, string UserId, DateTimeOffset Date, decimal Weight)
{
    public static WeighingResponse FromEntity(Weighing weighing) =>
        new(weighing.WeighingId, weighing.UserId, weighing.Date, weighing.Weight);
}

internal record UpsertWeighingRequest(DateTimeOffset Date, decimal Weight)
{
    [JsonIgnore]
    public string UserId { get; private set; } = string.Empty;

    public UpsertWeighingRequest AppendUserId(string userId) => this with { UserId = userId };

    public Weighing ToEntity() => Weighing.Create(UserId, Date, Weight);
}

internal record GetWeighingByDateRequest(string DateString, string UserId);

internal record DeleteWeighingRequest(string DateString, string UserId);
