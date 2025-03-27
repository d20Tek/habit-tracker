using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Categories;

internal class CreateCategoryCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public CreateCategoryCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<CategoryResponse>> Handle(CreateCategoryRequest request) => 
        await TryExcept.RunAsync(
            async () => await Validate(request)
                                .MapAsync(async () => await CreateEntity(request.ToEntity())),
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(CreateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Categories.CreateName))
                        .AddIfError(() => request.UserId.Length > Constants.Categories.UserIdLength,
                                  Constants.UserIdLengthError(Constants.Categories.CreateName))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Categories.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Categories.NameLength,
                                  Constants.Categories.NameLengthError);

    private async Task<CategoryResponse> CreateEntity(Category c)
    {
        var r = await _db.Categories.AddAsync(c);
        await _db.SaveChangesAsync();

        _cache.Remove(Constants.Categories.GetCacheKey(c.UserId));
        return CategoryResponse.FromEntity(r.Entity);
    }
}
