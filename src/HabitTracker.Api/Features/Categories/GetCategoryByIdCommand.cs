using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Categories;

internal class GetCategoryByIdCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public GetCategoryByIdCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await GetCategoryById(request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound(nameof(Category), request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(GetCategoryByIdRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.GetByIdName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Categories.GetByIdName));

    private async Task<Option<CategoryResponse>> GetCategoryById(GetCategoryByIdRequest request) =>
        (await _cache.GetOrCreateAsync(Constants.Categories.GetByIdCacheKey(request.Id, request.UserId), async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Constants.Categories.CacheExpiration;

            var cat = await _db.Categories.SingleOrDefaultAsync(
                x => x.CategoryId == request.Id && x.UserId == request.UserId);

            return (cat is null) ? null : CategoryResponse.FromEntity(cat);
        })).ToOption();
}
