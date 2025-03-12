namespace HabitTracker.Api.Features.Categories;

internal class GetCategoryByIdCommand
{
    private readonly AppDbContext _db;

    public GetCategoryByIdCommand(AppDbContext db) => _db = db;

    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await GetCategoryById(_db, request);
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

    private static async Task<Option<CategoryResponse>> GetCategoryById(
        AppDbContext db,
        GetCategoryByIdRequest request)
    {
        var cat = await db.Categories.SingleOrDefaultAsync(
            x => x.CategoryId == request.Id && x.UserId == request.UserId);

        return (cat is null) ? Option<CategoryResponse>.None() : CategoryResponse.FromEntity(cat);
    }
}
