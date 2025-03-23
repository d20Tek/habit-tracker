namespace HabitTracker.Api.Features.Weighings;

internal static class GetWeighingByIdEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Weighings.ServiceBaseWithId, Get)
              .WithTags(nameof(Weighing))
              .WithName(Constants.Weighings.GetByIdName)
              .WithDescription(Constants.Weighings.GetByIdDesc)
              .Produces<WeighingResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Get(
        [FromRoute] int id,
        [FromServices] GetWeighingByIdCommand command,
        [FromServices] ILogger<GetWeighingByIdCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Weighings.GetByIdName);
        var result = await command.Handle(new(id, user.GetId()));
        logger.LogEndpointComplete(Constants.Weighings.GetByIdName, result);
        return result.ToApiResult();
    }
}
