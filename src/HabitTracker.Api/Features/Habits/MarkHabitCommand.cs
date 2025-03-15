namespace HabitTracker.Api.Features.Habits;

internal class MarkHabitCommand
{
    private readonly AppDbContext _db;

    public MarkHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(MarkHabitRequest request) => throw new NotImplementedException();
}
