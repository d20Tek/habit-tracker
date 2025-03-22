namespace HabitTracker.Api.Features.Weighings;

internal static class UpsertWeighingEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPut(Constants.Weighings.ServiceBase, Upsert)
              .WithTags(nameof(Weighing))
              .WithName(Constants.Weighings.UpsertName)
              .WithDescription(Constants.Weighings.UpsertDesc)
              .Produces<WeighingResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Upsert(
        [FromBody] UpsertWeighingRequest request,
        [FromServices] UpsertWeighingCommand command,
        [FromServices] ILogger<UpsertWeighingCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Weighings.UpsertName);
        var result = await command.Handle(request.AppendUserId(user.GetId()));
        logger.LogEndpointComplete(Constants.Weighings.UpsertName, result);
        return result.ToApiResult();
    }
}
