namespace HabitTracker.Web.Features.Weighings;

internal record WeighingResponse(int Id, DateTimeOffset Date, decimal Weight);

internal record UpsertWeighingRequest(DateTimeOffset Date, decimal Weight);
