using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Categories;

internal class GetCategoriesForUserCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public GetCategoriesForUserCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<IList<CategoryResponse>>> Handle(string userId) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                .MapAsync(() => GetEntitiesForUser(userId)),
            ex => Result<IList<CategoryResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Categories.GetAllName));

    private async Task<IList<CategoryResponse>> GetEntitiesForUser(string userId) =>
        await _cache.GetOrCreateAsync(Constants.Categories.GetCacheKey(userId), async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Constants.Categories.CacheExpiration;

            return await _db.Categories.Where(c => c.UserId == userId)
                           .Select(c => new CategoryResponse(c.CategoryId, c.Name, c.UserId))
                           .AsNoTracking()
                           .ToListAsync();

        }) ?? [];
}
