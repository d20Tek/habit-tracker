namespace HabitTracker.Api.Features.Categories;

internal class CreateCategoryCommand
{
    private readonly AppDbContext _db;

    public CreateCategoryCommand(AppDbContext db) => _db = db;

    public async Task<Result<CategoryResponse>> Handle(CreateCategoryRequest request) => 
        await TryExcept.RunAsync(
            async () => await Validate(request)
                                .MapAsync(async () => await CreateEntity(_db, request.ToEntity())),
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

    private static async Task<CategoryResponse> CreateEntity(AppDbContext db, Category c)
    {
        var r = await db.Categories.AddAsync(c);
        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(r.Entity);
    }
}
