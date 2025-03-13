namespace HabitTracker.Api.Features.Habits;

internal class UpdateHabitCommand
{
    private readonly AppDbContext _db;

    public UpdateHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(UpdateHabitRequest request) =>
        throw new NotImplementedException();
}
