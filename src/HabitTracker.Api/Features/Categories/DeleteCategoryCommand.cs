namespace HabitTracker.Api.Features.Categories;

internal class DeleteCategoryCommand
{
    private readonly AppDbContext _db;

    public DeleteCategoryCommand(AppDbContext db) => _db = db;

    public async Task<Result<CategoryResponse>> Handle(DeleteCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(_db, request);
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

    private static async Task<Option<CategoryResponse>> DeleteEntity(AppDbContext db, DeleteCategoryRequest request)
    {
        var c = await db.Categories.SingleOrDefaultAsync(x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (c is null) return Option<CategoryResponse>.None();

        var result = db.Categories.Remove(c);
        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(result.Entity);
    }
}
