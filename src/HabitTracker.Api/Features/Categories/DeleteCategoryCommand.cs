using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal static class DeleteCategoryCommand
{
    public static async Task<Result<CategoryResponse>> Handle(AppDbContext db, DeleteCategoryRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = request.Validate();
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(db, request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound("Category", request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(this DeleteCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  "DeleteCategory.Id",
                                  "Category id must be a valid number.")
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("DeleteCategory"));

    private static async Task<Option<CategoryResponse>> DeleteEntity(AppDbContext db, DeleteCategoryRequest request)
    {
        var c = await db.Categories.SingleOrDefaultAsync(x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (c is null) return Option<CategoryResponse>.None();

        var result = db.Categories.Remove(c);
        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(result.Entity);
    }
}
