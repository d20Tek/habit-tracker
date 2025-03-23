namespace HabitTracker.Api.Features.Weighings;

internal class DeleteWeighingCommand
{
    private readonly AppDbContext _db;

    public DeleteWeighingCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(DeleteWeighingRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<WeighingResponse>.Failure(validations.ToArray());

                var result = await DeleteEntity(_db, request);
                return result.Match(
                    response => response,
                    () => Result<WeighingResponse>.Failure(Constants.Weighings.WeighingNotFound));
            },
            ex => Result<WeighingResponse>.Failure(ex));

    private static ValidationErrors Validate(DeleteWeighingRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.WeighingId <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.DeleteName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.DeleteName));

    private static async Task<Option<WeighingResponse>> DeleteEntity(AppDbContext db, DeleteWeighingRequest request)
    {
        var w = await db.Weighings.SingleOrDefaultAsync(
            x => x.WeighingId == request.WeighingId && x.UserId == request.UserId);
        if (w is null) return Option<WeighingResponse>.None();

        var result = db.Weighings.Remove(w);
        await db.SaveChangesAsync();
        return WeighingResponse.FromEntity(result.Entity);
    }
}
