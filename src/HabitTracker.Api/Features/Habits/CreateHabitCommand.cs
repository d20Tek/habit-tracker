namespace HabitTracker.Api.Features.Habits;

internal class CreateHabitCommand
{
    private readonly AppDbContext _db;

    public CreateHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(CreateHabitRequest request) =>
        await TryExcept.RunAsync(
            async () => await Validate(request)
                                .MapAsync(async () => await CreateEntity(_db, request.ToEntity())),
            ex => Result<HabitResponse>.Failure(ex));

    private static ValidationErrors Validate(CreateHabitRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Habits.CreateName))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  Constants.Habits.RequiredNameError)
                        .AddIfError(() => request.Name.Length > Constants.Habits.NameLength,
                                  Constants.Habits.NameLengthError)
                        .AddIfError(() => request.Description is not null &&
                                          request.Description.Length > Constants.Habits.DescLength,
                                  Constants.Habits.DescLengthError)
                        .AddIfError(() => request.TargetAttempts <= 0,
                                  Constants.Habits.TargetAttemptsError)
                        .AddIfError(() => request.CategoryId <= 0,
                                  Constants.Habits.TargetAttemptsError);

    private static async Task<HabitResponse> CreateEntity(AppDbContext db, Habit habit)
    {
        var r = await db.Habits.AddAsync(habit);
        await db.SaveChangesAsync();
        await db.Entry(r.Entity).Reference(h => h.Category).LoadAsync();

        return HabitResponse.FromEntity(r.Entity, []);
    }
}
