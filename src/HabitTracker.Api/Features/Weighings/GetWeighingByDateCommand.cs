namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingByDateCommand
{
    private readonly AppDbContext _db;

    public GetWeighingByDateCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(GetWeighingByDateRequest request) =>
        throw new NotImplementedException();
}
