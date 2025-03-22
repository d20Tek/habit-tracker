namespace HabitTracker.Api.Features.Weighings;

internal class UpsertWeighingCommand
{
    private readonly AppDbContext _db;

    public UpsertWeighingCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(UpsertWeighingRequest request) =>
        throw new NotImplementedException();
}
