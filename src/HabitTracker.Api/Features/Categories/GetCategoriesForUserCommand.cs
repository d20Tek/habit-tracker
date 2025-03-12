using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Features.Categories;

internal class GetCategoriesForUserCommand
{
    private readonly AppDbContext _db;

    public GetCategoriesForUserCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<CategoryResponse>>> Handle(string userId) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                .MapAsync(() => GetEntitiesForUser(_db, userId)),
            ex => Result<IList<CategoryResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Categories.GetAllName));

    private static async Task<IList<CategoryResponse>> GetEntitiesForUser(
        AppDbContext db,
        string userId) =>
        await db.Categories.Where(c => c.UserId == userId)
                           .Select(c => new CategoryResponse(c.CategoryId, c.Name, c.UserId))
                           .AsNoTracking()
                           .ToListAsync();
}
