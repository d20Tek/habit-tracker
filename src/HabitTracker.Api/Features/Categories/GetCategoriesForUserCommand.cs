using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;

namespace HabitTracker.Api.Features.Categories;

internal static class GetCategoriesForUserCommand
{
    public static Result<IList<CategoryResponse>> Handle(HabitTrackerDbContext db, string userId) =>
        TryExcept.Run(
            () => userId.Validate()
                        .Map(_ => db.Categories.Where(c => c.UserId == userId)
                                               .Select(c => new CategoryResponse(c.CategoryId, c.Name, c.UserId))
                                               .ToList())
                        .GetValue(),
            ex => Result<IList<CategoryResponse>>.Failure(ex));

    private static Result<bool> Validate(this string userId) =>
        string.IsNullOrEmpty(userId) ?
            Result<bool>.Failure(Constants.UserIdRequiredError("GetCategories")) :
            true;
}
