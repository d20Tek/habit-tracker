using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence;

namespace HabitTracker.Api.Features.Categories;

internal static class CreateCategoryCommand
{
    public static async Task<Result<CategoryResponse>> Handle(HabitTrackerDbContext db, CreateCategoryRequest request) => 
        await TryExcept.RunAsync(
            async () => await request.Validate()
                                     .MapAsync(async () => await CreateEntity(db, request.ToEntity())),
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(this CreateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("CreateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  "CreateCategory.Name",
                                  "Category name is a required.")
                        .AddIfError(() => request.Name.Length > 100,
                                  "CreateCategory.Name",
                                  "Category name must be less than 100 characters.");

    private static async Task<CategoryResponse> CreateEntity(HabitTrackerDbContext db, Category c)
    {
        var r = await db.Categories.AddAsync(c);
        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(r.Entity);
    }
}
