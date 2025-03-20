namespace HabitTracker.Api.Features.Categories;

internal class UpdateCategoryCommand
{
    private readonly AppDbContext _db;

    public UpdateCategoryCommand(AppDbContext db) => _db = db;

    public async Task<Result<CategoryResponse>> Handle(UpdateCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await UpdateEntity(_db, request);
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

    private static async Task<Option<CategoryResponse>> UpdateEntity(AppDbContext db, UpdateCategoryRequest request)
    {
        var cat = await db.Categories.SingleOrDefaultAsync(
            x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (cat is null) return Option<CategoryResponse>.None();

        cat.Rename(request.Name);

        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(cat);
    }
}
