namespace HabitTracker.Api.Features.Habits;

internal class GetHabitByIdCommand
{
    private readonly AppDbContext _db;

    public GetHabitByIdCommand(AppDbContext db) => _db = db;

    public async Task<Result<HabitResponse>> Handle(GetHabitByIdRequest request) =>
        throw new NotImplementedException();
}
