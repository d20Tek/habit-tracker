namespace HabitTracker.Web.Features.Weighings;

internal record WeighingResponse(DateTimeOffset Date, decimal Weight);

internal record UpsertWeighingRequest(DateTimeOffset Date, decimal Weight);
