using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Categories;

internal class UpdateCategoryCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public UpdateCategoryCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<CategoryResponse>> Handle(UpdateCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await UpdateEntity(request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound(nameof(Category), request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(UpdateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.UpdateName))
                        .AddIfError(() => request.UserId.Length > Constants.Categories.UserIdLength,
                                  Constants.UserIdLengthError(Constants.Categories.UpdateName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Categories.UpdateName))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Categories.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Categories.NameLength,
                                  Constants.Categories.NameLengthError);

    private async Task<Option<CategoryResponse>> UpdateEntity(UpdateCategoryRequest request)
    {
        var cat = await _db.Categories.SingleOrDefaultAsync(
            x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (cat is null) return Option<CategoryResponse>.None();

        cat.Rename(request.Name);
        await _db.SaveChangesAsync();

        _cache.Remove(Constants.Categories.GetCacheKey(request.UserId));
        _cache.Remove(Constants.Categories.GetByIdCacheKey(request.Id, request.UserId));
        return CategoryResponse.FromEntity(cat);
    }
}
