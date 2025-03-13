namespace HabitTracker.Api.Features.Habits;

internal class DeleteHabitCommand
{
    private readonly AppDbContext _db;

    public DeleteHabitCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(DeleteHabitRequest request) =>
        throw new NotImplementedException();
}
