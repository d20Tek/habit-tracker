namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingsForUserCommand
{
    private readonly AppDbContext _db;

    public GetWeighingsForUserCommand(AppDbContext db) => _db = db;

    public async Task<Result<IList<WeighingResponse>>> Handle(string userId) =>
        await TryExcept.RunAsync(
            async () => await Validate(userId)
                                .MapAsync(() => GetEntitiesForUser(_db, userId)),
            ex => Result<IList<WeighingResponse>>.Failure(ex));

    private static ValidationErrors Validate(string userId) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(userId),
                            Constants.UserIdRequiredError(Constants.Weighings.GetAllName));

    private static async Task<IList<WeighingResponse>> GetEntitiesForUser(AppDbContext db, string userId) =>
        await db.Weighings.Where(w => w.UserId == userId)
                          .Take(Constants.Weighings.DefaultLimit)
                          .AsNoTracking()
                          .Select(w => new WeighingResponse(w.WeighingId, w.UserId, w.Date, w.Weight))
                          .ToListAsync();
}
