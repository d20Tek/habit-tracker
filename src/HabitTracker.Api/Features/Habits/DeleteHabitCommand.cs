namespace HabitTracker.Api.Features.Habits;

internal class DeleteHabitCommand
{
    private readonly AppDbContext _db;

    public DeleteHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(DeleteHabitRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<HabitResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(_db, request);
                return result.Match(
                    response => response,
                    () => Result<HabitResponse>.Failure(Constants.EntityNotFound(nameof(Habit), request.Id)));
            },
            ex => Result<HabitResponse>.Failure(ex));

    private static ValidationErrors Validate(DeleteHabitRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(Constants.Habits.DeleteName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Habits.DeleteName));

    private static async Task<Option<HabitResponse>> DeleteEntity(AppDbContext db, DeleteHabitRequest request)
    {
        var h = await db.Habits.SingleOrDefaultAsync(x => x.HabitId == request.Id && x.UserId == request.UserId);
        if (h is null) return Option<HabitResponse>.None();

        var result = db.Habits.Remove(h);
        await db.SaveChangesAsync();
        return HabitResponse.FromEntity(result.Entity);
    }
}
