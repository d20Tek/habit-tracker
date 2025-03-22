namespace HabitTracker.Api.Features.Weighings;

internal static class GetWeighingByDateEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Weighings.ServiceBaseWithId, Get)
              .WithTags(nameof(Weighing))
              .WithName(Constants.Weighings.GetByDateName)
              .WithDescription(Constants.Weighings.GetByDateDesc)
              .Produces<WeighingResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Get(
        [FromRoute] string date,
        [FromServices] GetWeighingByDateCommand command,
        [FromServices] ILogger<GetWeighingByDateCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.GetByIdName);
        var result = await command.Handle(new(date, user.GetId()));
        logger.LogEndpointComplete(Constants.Categories.GetByIdName, result);
        return result.ToApiResult();
    }
}
