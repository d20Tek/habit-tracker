using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingsForUserCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public GetWeighingsForUserCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<IList<WeighingResponse>>> Handle(string userId) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                .MapAsync(() => GetEntitiesForUser(userId)),
            ex => Result<IList<WeighingResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Weighings.GetAllName));

    private async Task<IList<WeighingResponse>> GetEntitiesForUser(string userId) =>
        await _cache.GetOrCreateAsync(Constants.Weighings.GetCacheKey(userId), async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Constants.Weighings.CacheExpiration;

            return await _db.Weighings.Where(w => w.UserId == userId)
                                      .OrderByDescending(w => w.Date)
                                      .Take(Constants.Weighings.DefaultLimit)
                                      .AsNoTracking()
                                      .Select(w => new WeighingResponse(w.WeighingId, w.UserId, w.Date, w.Weight))
                                      .ToListAsync();
        }) ?? [];
}
