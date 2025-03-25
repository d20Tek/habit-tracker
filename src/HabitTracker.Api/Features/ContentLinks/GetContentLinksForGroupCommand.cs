using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.ContentLinks;

internal class GetContentLinksForGroupCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public GetContentLinksForGroupCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<IList<ContentLinkResponse>>> Handle(GetContentLinksForGroupRequest request) =>
        await TryExcept.RunAsync(
            async () => await Validate(request)
                                .MapAsync(() => GetEntitiesForGroup(_db, request)),
            ex => Result<IList<ContentLinkResponse>>.Failure(ex));

    private static ValidationErrors Validate(GetContentLinksForGroupRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.Group),
                                    Constants.ContentLinks.RequiredGroupError);

    private async Task<IList<ContentLinkResponse>> GetEntitiesForGroup(
        AppDbContext db,
        GetContentLinksForGroupRequest request)
    {
        var cacheKey = Constants.ContentLinks.GetCacheKey(request.Group);
        if (!_cache.TryGetValue(cacheKey, out List<ContentLinkResponse>? links))
        {
            links = await db.ContentLinks.Where(c => c.Group == request.Group)
                                         .OrderBy(c => c.SortOrder)
                                         .Take(Constants.ContentLinks.GroupLinkLimit)
                                         .AsNoTracking()
                                         .Select(c => ContentLinkResponse.FromEntity(c))
                                         .ToListAsync();

            _cache.Set(cacheKey, links, Constants.ContentLinks.CacheExpiration);
        }

        return links ?? [];
    }
}
