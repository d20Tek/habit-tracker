using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal static class GetCategoriesForUserCommand
{
    public static async Task<Result<IList<CategoryResponse>>> Handle(HabitTrackerDbContext db, string userId) =>
        await TryExcept.RunAsync(
            async () => await userId.Validate()
                                    .MapAsync(() => GetEntitiesForUser(db, userId)),
            ex => Result<IList<CategoryResponse>>.Failure(ex));

    private static ValidationErrors Validate(this string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError("GetCategories"));

    private static async Task<IList<CategoryResponse>> GetEntitiesForUser(
        HabitTrackerDbContext db,
        string userId) =>
        await db.Categories.Where(c => c.UserId == userId)
                           .Select(c => new CategoryResponse(c.CategoryId, c.Name, c.UserId))
                           .ToListAsync();
}
