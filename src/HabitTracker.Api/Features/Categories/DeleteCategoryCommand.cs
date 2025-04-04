using Microsoft.Extensions.Caching.Memory;

namespace HabitTracker.Api.Features.Categories;

internal class DeleteCategoryCommand
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _db;

    public DeleteCategoryCommand(IMemoryCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Result<CategoryResponse>> Handle(DeleteCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound(nameof(Category), request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(DeleteCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.DeleteName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Categories.DeleteName));

    private async Task<Option<CategoryResponse>> DeleteEntity(DeleteCategoryRequest request)
    {
        var c = await _db.Categories.SingleOrDefaultAsync(
            x => x.CategoryId == request.Id && x.UserId == request.UserId);

        if (c is null) return Option<CategoryResponse>.None();

        var result = _db.Categories.Remove(c);
        await _db.SaveChangesAsync();

        _cache.Remove(Constants.Categories.GetCacheKey(request.UserId));
        _cache.Remove(Constants.Categories.GetByIdCacheKey(request.Id, request.UserId));
        return CategoryResponse.FromEntity(result.Entity);
    }
}
