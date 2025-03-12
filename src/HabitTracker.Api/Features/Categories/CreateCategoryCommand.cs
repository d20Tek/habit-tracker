namespace HabitTracker.Api.Features.Categories;

internal static class CreateCategoryCommand
{
    public static async Task<Result<CategoryResponse>> Handle(AppDbContext db, CreateCategoryRequest request) => 
        await TryExcept.RunAsync(
            async () => await request.Validate()
                                     .MapAsync(async () => await CreateEntity(db, request.ToEntity())),
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(this CreateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("CreateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Categories.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Categories.NameLength,
                                  Constants.Categories.NameLengthError);

    private static async Task<CategoryResponse> CreateEntity(AppDbContext db, Category c)
    {
        var r = await db.Categories.AddAsync(c);
        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(r.Entity);
    }
}
