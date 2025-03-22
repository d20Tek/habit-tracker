namespace HabitTracker.Api.Features.Weighings;

internal static class DeleteWeighingEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapDelete(Constants.Weighings.ServiceBaseWithDate, Delete)
              .WithTags(nameof(Weighing))
              .WithName(Constants.Weighings.DeleteName)
              .WithDescription(Constants.Weighings.DeleteDesc)
              .Produces<WeighingResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Delete(
        [FromRoute] string date,
        [FromServices] DeleteWeighingCommand command,
        [FromServices] ILogger<DeleteWeighingCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Weighings.DeleteName);
        var result = await command.Handle(new(date, user.GetId()));
        logger.LogEndpointComplete(Constants.Weighings.DeleteName, result);
        return result.ToApiResult();
    }
}
