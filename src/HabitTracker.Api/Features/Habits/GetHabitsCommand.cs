namespace HabitTracker.Api.Features.Habits;

internal class GetHabitsCommand
{
    private readonly AppDbContext _db;

    public GetHabitsCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<HabitResponse>>> Handle(string userId, int takeCompletions = 1) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                  .MapAsync(() => GetEntitiesForUser(_db, userId, takeCompletions)),
            ex => Result<IList<HabitResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Habits.GetAllName));

    private static async Task<IList<HabitResponse>> GetEntitiesForUser(
        AppDbContext db,
        string userId,
        int takeCompletions) =>
        await db.Habits.QueryHabitsForUser(userId, takeCompletions)
                       .ToListAsync();
}
