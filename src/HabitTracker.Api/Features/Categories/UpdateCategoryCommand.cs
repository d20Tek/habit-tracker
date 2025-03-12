using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal static class UpdateCategoryCommand
{
    public static async Task<Result<CategoryResponse>> Handle(HabitTrackerDbContext db, UpdateCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = request.Validate();
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await UpdateEntity(db, request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound("Category", request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(this UpdateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  "UpdateCategory.Id",
                                  "Category id must be a valid number.")
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("UpdateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  "UpdateCategory.Name",
                                  "Category name is a required.")
                        .AddIfError(() => request.Name.Length > 100,
                                  "UpdateCategory.Name",
                                  "Category name must be less than 100 characters.");

    private static async Task<Option<CategoryResponse>> UpdateEntity(HabitTrackerDbContext db, UpdateCategoryRequest request)
    {
        var cat = await db.Categories.SingleOrDefaultAsync(x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (cat is null) return Option<CategoryResponse>.None();

        cat.Name = request.Name;

        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(cat);
    }
}
