using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Weighings;

internal class DeleteWeighingCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public DeleteWeighingCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<WeighingResponse>> Handle(DeleteWeighingRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<WeighingResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(request);
                return result.Match(
                    response => response,
                    () => Result<WeighingResponse>.Failure(Constants.Weighings.WeighingNotFound));
            },
            ex => Result<WeighingResponse>.Failure(ex));

    private static ValidationErrors Validate(DeleteWeighingRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.WeighingId <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.DeleteName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.DeleteName));

    private async Task<Option<WeighingResponse>> DeleteEntity(DeleteWeighingRequest request)
    {
        var w = await _db.Weighings.SingleOrDefaultAsync(
            x => x.WeighingId == request.WeighingId && x.UserId == request.UserId);
        if (w is null) return Option<WeighingResponse>.None();

        var result = _db.Weighings.Remove(w);
        await _db.SaveChangesAsync();

        _cache.Remove(Constants.Weighings.GetCacheKey(request.UserId));
        return WeighingResponse.FromEntity(result.Entity);
    }
}
