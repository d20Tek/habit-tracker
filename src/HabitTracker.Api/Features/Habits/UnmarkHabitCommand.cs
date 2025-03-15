namespace HabitTracker.Api.Features.Habits;

internal class UnmarkHabitCommand
{
    private readonly AppDbContext _db;

    public UnmarkHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(UnmarkHabitRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
        var validations = Validate(request);
        if (validations.HasErrors) return Result<HabitResponse>.Failure(validations.ToArray());

        var result = await UpdateEntity(_db, request);
        return result.Match(
            response => response,
            () => Result<HabitResponse>.Failure(Constants.EntityNotFound(nameof(Habit), request.Id)));
    },
            ex => Result<HabitResponse>.Failure(ex));

    private static ValidationErrors Validate(UnmarkHabitRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.HabitCompletions.MarkName))
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(nameof(Habit)))
                        .AddIfError(() => request.Date > DateTimeOffset.Now,
                                  Constants.HabitCompletions.FutureDateError)
                        .AddIfError(() => request.Decrement < 1 || request.Decrement > 100,
                                  Constants.HabitCompletions.IncrementRangeError);

    private static async Task<Option<HabitResponse>> UpdateEntity(AppDbContext db, UnmarkHabitRequest request)
    {
        var habit = await db.Habits.Include(h => h.DailyCompletions)
                                   .SingleOrDefaultAsync(x => x.HabitId == request.Id && x.UserId == request.UserId);
        if (habit is null) return Option<HabitResponse>.None();

        habit.UnmarkCompleted(request.Date, request.Decrement);
        await db.SaveChangesAsync();

        var updated = await db.Habits.Include(h => h.Category)
                                     .Include(h => h.DailyCompletions)
                                     .SingleAsync(x => x.HabitId == request.Id && x.UserId == request.UserId);
        return HabitResponse.FromEntity(updated);
    }
}
