namespace HabitTracker.Api.Features.Habits;

internal class GetHabitByIdCommand
{
    private readonly AppDbContext _db;

    public GetHabitByIdCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(GetHabitByIdRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<HabitResponse>.Failure(validations.ToArray());

                var result = await GetHabitById(_db, request);
                return result.Match(
                    response => response,
                    () => Result<HabitResponse>.Failure(Constants.EntityNotFound(nameof(Habit), request.Id)));
            },
            ex => Result<HabitResponse>.Failure(ex));

    private static ValidationErrors Validate(GetHabitByIdRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.Id <= 0,
                                  Constants.EntityIdRequiredError(Constants.Habits.GetByIdName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Habits.GetByIdName));

    private static async Task<Option<HabitResponse>> GetHabitById(AppDbContext db, GetHabitByIdRequest request) =>
        await db.Habits.QueryHabitById(request.Id, request.UserId, request.LimitCompletions)
                       .SingleAsync();
}
