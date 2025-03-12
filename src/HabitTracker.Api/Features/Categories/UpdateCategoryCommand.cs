using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal static class UpdateCategoryCommand
{
    public static async Task<Result<CategoryResponse>> Handle(AppDbContext db, UpdateCategoryRequest request) =>
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
                                  Constants.EntityIdRequiredError("UpdateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("UpdateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Categories.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Categories.NameLength,
                                  Constants.Categories.NameLengthError);

    private static async Task<Option<CategoryResponse>> UpdateEntity(AppDbContext db, UpdateCategoryRequest request)
    {
        var cat = await db.Categories.SingleOrDefaultAsync(x => x.CategoryId == request.Id && x.UserId == request.UserId);
        if (cat is null) return Option<CategoryResponse>.None();

        cat.Rename(request.Name);

        await db.SaveChangesAsync();
        return CategoryResponse.FromEntity(cat);
    }
}
