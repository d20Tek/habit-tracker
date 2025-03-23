namespace HabitTracker.Api.Features.Weighings;

internal class GetWeighingByIdCommand
{
    private readonly AppDbContext _db;

    public GetWeighingByIdCommand(AppDbContext db) => _db = db;

    public async Task<Result<WeighingResponse>> Handle(GetWeighingByIdRequest request) =>
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

    private static ValidationErrors Validate(GetWeighingByIdRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => request.WeighingId <= 0,
                                  Constants.EntityIdRequiredError(Constants.Categories.DeleteName))
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError(Constants.Weighings.GetByIdName));

    private static async Task<Option<WeighingResponse>> GetCategoryById(
        AppDbContext db,
        GetWeighingByIdRequest request)
    {
        var weighing = await db.Weighings.SingleOrDefaultAsync(
            x => x.WeighingId == request.WeighingId && x.UserId == request.UserId);

        return (weighing is null) ? Option<WeighingResponse>.None() : WeighingResponse.FromEntity(weighing);
    }
}
