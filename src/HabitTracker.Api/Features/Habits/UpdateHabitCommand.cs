namespace HabitTracker.Api.Features.Habits;

internal class UpdateHabitCommand
{
    private readonly AppDbContext _db;

    public UpdateHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(UpdateHabitRequest request) =>
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

    private static ValidationErrors Validate(UpdateHabitRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Habits.UpdateName))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Habits.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Habits.NameLength,
                                  Constants.Habits.NameLengthError)
                        .AddIfError(() => request.Description is not null &&
                                          request.Description.Length > Constants.Habits.DescLength,
                                  Constants.Habits.DescLengthError)
                        .AddIfError(() => request.TargetAttempts <= 0,
                                  Constants.Habits.TargetAttemptsError);

    private static async Task<Option<HabitResponse>> UpdateEntity(AppDbContext db, UpdateHabitRequest request)
    {
        var habit = await db.Habits.SingleOrDefaultAsync(x => x.HabitId == request.Id && x.UserId == request.UserId);
        if (habit is null) return Option<HabitResponse>.None();

        habit.Name = request.Name;
        habit.Description = request.Description;
        habit.TargetAttempts = request.TargetAttempts;
        habit.CategoryId = request.CategoryId;

        await db.SaveChangesAsync();
        var updated = await db.Habits.Include(h => h.Category)
                                     .SingleAsync(x => x.HabitId == request.Id && x.UserId == request.UserId);

        return HabitResponse.FromEntity(updated);
    }
}
