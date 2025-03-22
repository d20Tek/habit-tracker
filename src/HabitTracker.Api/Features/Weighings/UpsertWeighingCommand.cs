namespace HabitTracker.Api.Features.Weighings;

internal class UpsertWeighingCommand
{
    private readonly AppDbContext _db;

    public UpsertWeighingCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(UpsertWeighingRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                return validations.HasErrors ?
                    Result<WeighingResponse>.Failure(validations.ToArray()) :
                    await UpsertEntity(_db, request);
            },
            ex => Result<WeighingResponse>.Failure(ex));

    private static ValidationErrors Validate(UpsertWeighingRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.UserId.Length > Constants.Categories.UserIdLength,
                                  Constants.UserIdLengthError(Constants.Weighings.UpsertName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.UpsertName))
                        .AddIfError(() => request.Date > DateTimeOffset.Now,
                                  Constants.Weighings.FutureDateError)
                        .AddIfError(() => request.Weight < Constants.Weighings.MinWeight ||
                                          request.Weight > Constants.Weighings.MaxWeight,
                                  Constants.Weighings.WeightError);

    private static async Task<WeighingResponse> UpsertEntity(AppDbContext db, UpsertWeighingRequest request)
    {
        var w = (await db.Weighings.SingleOrDefaultAsync(x => x.Date == request.Date)).ToOption();
        var weighing = await w.Match(wt => UpdateEntity(wt, request.Weight), () => CreateEntity(db, request));

        await db.SaveChangesAsync();
        return WeighingResponse.FromEntity(weighing);
    }

    private static async Task<Weighing> CreateEntity(AppDbContext db, UpsertWeighingRequest request) =>
        (await db.Weighings.AddAsync(request.ToEntity())).Entity;

    private static Task<Weighing> UpdateEntity(Weighing weighing, decimal newWeight)
    {
        weighing.ChangeWeight(newWeight);
        return Task.FromResult(weighing);
    }
}
