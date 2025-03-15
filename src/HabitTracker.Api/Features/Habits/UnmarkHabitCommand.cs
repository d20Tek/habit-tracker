namespace HabitTracker.Api.Features.Habits;

internal class UnmarkHabitCommand
{
    private readonly AppDbContext _db;

    public UnmarkHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(UnmarkHabitRequest request) => throw new NotImplementedException();
}
