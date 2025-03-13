namespace HabitTracker.Api.Features.Habits;

internal class GetHabitsCommand
{
    private readonly AppDbContext _db;

    public GetHabitsCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<HabitResponse>>> Handle(string userId) => throw new NotImplementedException();
}
