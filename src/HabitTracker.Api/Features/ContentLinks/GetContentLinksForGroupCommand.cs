using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.ContentLinks;

internal class GetContentLinksForGroupCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;
    private readonly Random _random;

    public GetContentLinksForGroupCommand(IMemoryCache cache, AppDbContext db, Random rnd)
    {
        _cache = cache;
        _db = db;
        _random = rnd;
    }

    public async Task<Result<IList<ContentLinkResponse>>> Handle(GetContentLinksForGroupRequest request) =>
        await TryExcept.RunAsync(
            async () => await Validate(request)
                                .MapAsync(() => GetEntitiesForGroup(request)),
            ex => Result<IList<ContentLinkResponse>>.Failure(ex));

    private static ValidationErrors Validate(GetContentLinksForGroupRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.Group),
                                    Constants.ContentLinks.RequiredGroupError);

    private async Task<IList<ContentLinkResponse>> GetEntitiesForGroup(GetContentLinksForGroupRequest request) =>
        await _cache.GetOrCreateAsync(Constants.ContentLinks.GetCacheKey(request.Group), async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Constants.ContentLinks.CacheExpiration;

            var links =  await _db.ContentLinks.Where(c => c.Group == request.Group)
                                               .OrderBy(c => c.SortOrder)
                                               .Take(Constants.ContentLinks.GroupLinkMaxLimit)
                                               .AsNoTracking()
                                               .ToArrayAsync();
            return GetRandomizedList(links);
        }) ?? [];

    private List<ContentLinkResponse> GetRandomizedList(ContentLink[] links)
    {
        var randomLinks = links.OrderBy(_ => _random.Next())
                               .Take(Constants.ContentLinks.GroupLinkLimit);

        var result = randomLinks.OrderBy(c => c.SortOrder)
                                .Select(c => ContentLinkResponse.FromEntity(c))
                                .ToList();

        return result;
    }
}
