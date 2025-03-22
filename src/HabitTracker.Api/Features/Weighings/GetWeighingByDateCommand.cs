namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingByDateCommand
{
    private readonly AppDbContext _db;

    public GetWeighingByDateCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(GetWeighingByDateRequest request) =>
        await TryExcept.RunAsync(
            async () =>
            {
                var validations = Validate(request);
                if (validations.HasErrors) return Result<WeighingResponse>.Failure(validations.ToArray());

                var result = await GetCategoryById(_db, request);
                return result.Match(
                    response => response,
                    () => Result<WeighingResponse>.Failure(Constants.Weighings.WeighingNotFound));
            },
            ex => Result<WeighingResponse>.Failure(ex));

    private static ValidationErrors Validate(GetWeighingByDateRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.GetByDateName))
                        .AddIfError(() => DateTimeOffset.TryParse(request.DateString, out _),
                                  Constants.Weighings.InvalidDateFormat);

    private static async Task<Option<WeighingResponse>> GetCategoryById(
        AppDbContext db,
        GetWeighingByDateRequest request)
    {
        var date = DateTimeOffset.Parse(request.DateString).Date;
        var weighing = await db.Weighings.SingleOrDefaultAsync(x => x.Date == date && x.UserId == request.UserId);
        return (weighing is null) ? Option<WeighingResponse>.None() : WeighingResponse.FromEntity(weighing);
    }
}
