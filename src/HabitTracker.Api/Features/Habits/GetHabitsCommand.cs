namespace HabitTracker.Api.Features.Habits;

internal class GetHabitsCommand
{
    private readonly AppDbContext _db;

    public GetHabitsCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<HabitResponse>>> Handle(string userId, int limitCompletions) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                  .MapAsync(() => GetEntitiesForUser(_db, userId, limitCompletions)),
            ex => Result<IList<HabitResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Habits.GetAllName));

    private static async Task<IList<HabitResponse>> GetEntitiesForUser(
        AppDbContext db, string userId, int limitCompletions) =>
        await db.Habits.QueryHabitsForUser(userId, limitCompletions)
                       .ToListAsync();
}
