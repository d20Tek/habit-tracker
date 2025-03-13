namespace HabitTracker.Api.Features.Habits;

internal class CreateHabitCommand
{
    private readonly AppDbContext _db;

    public CreateHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(CreateHabitRequest request) =>
        throw new NotImplementedException();
}
