namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingsForUserCommand
{
    private readonly AppDbContext _db;

    public GetWeighingsForUserCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<WeighingResponse>>> Handle(string userId) =>
        throw new NotImplementedException();
}
