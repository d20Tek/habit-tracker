using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Weighings;

internal class UpsertWeighingCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public UpsertWeighingCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<WeighingResponse>> Handle(UpsertWeighingRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                return validations.HasErrors ?
                    Result<WeighingResponse>.Failure(validations.ToArray()) :
                    await UpsertEntity(request);
            },
            ex => Result<WeighingResponse>.Failure(ex));

    private static ValidationErrors Validate(UpsertWeighingRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.UserId.Length > Constants.Categories.UserIdLength,
                                  Constants.UserIdLengthError(Constants.Weighings.UpsertName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.UpsertName))
                        .AddIfError(() => request.Date > DateTimeOffset.Now,
                                  Constants.Weighings.FutureDateError)
                        .AddIfError(() => request.Weight < Constants.Weighings.MinWeight ||
                                          request.Weight > Constants.Weighings.MaxWeight,
                                  Constants.Weighings.WeightError);

    private async Task<WeighingResponse> UpsertEntity(UpsertWeighingRequest request)
    {
        var w = (await _db.Weighings.SingleOrDefaultAsync(x => x.Date == request.Date && x.UserId == request.UserId))
                          .ToOption();
        Weighing weighing;
        if (w.IsSome)
        {
            weighing = w.Get();
            weighing.ChangeWeight(request.Weight);
        }
        else
        {
            weighing = (await _db.Weighings.AddAsync(request.ToEntity())).Entity;
        }

        await _db.SaveChangesAsync();

        _cache.Remove(Constants.Weighings.GetCacheKey(request.UserId));
        return WeighingResponse.FromEntity(weighing);
    }
}
