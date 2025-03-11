using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal static class GetCategoryByIdCommand
{
    public static async Task<Result<CategoryResponse>> Handle(HabitTrackerDbContext db, GetCategoryByIdRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = request.Validate();
                if (validations.HasErrors) return Result<CategoryResponse>.Failure(validations.ToArray());

                var result = await GetCategoryById(db, request);
                return result.Match(
                    response => response,
                    () => Result<CategoryResponse>.Failure(Constants.EntityNotFound("Category", request.Id)));
            },
            ex => Result<CategoryResponse>.Failure(ex));

    private static ValidationErrors Validate(this GetCategoryByIdRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  "GetCategoryById.Id",
                                  "Category id must be a valid number.")
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("GetCategoryById"));

    private static async Task<Option<CategoryResponse>> GetCategoryById(
        HabitTrackerDbContext db,
        GetCategoryByIdRequest request)
    {
        var cat = await db.Categories.SingleOrDefaultAsync(
            x => x.CategoryId == request.Id && x.UserId == request.UserId);

        return (cat is null) ? Option<CategoryResponse>.None() : CategoryResponse.FromEntity(cat);
    }
}
