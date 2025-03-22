namespace HabitTracker.Api.Features.Weighings;

internal class DeleteWeighingCommand
{
    private readonly AppDbContext _db;

    public DeleteWeighingCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(DeleteWeighingRequest request) =>
        throw new NotImplementedException();
}
