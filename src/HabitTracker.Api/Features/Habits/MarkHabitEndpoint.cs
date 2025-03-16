namespace HabitTracker.Api.Features.Habits;

internal static class MarkHabitEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPut(Constants.HabitCompletions.MarkServiceBase, Mark)
              .WithTags(nameof(Habit))
              .WithName(Constants.HabitCompletions.MarkName)
              .WithDescription(Constants.HabitCompletions.MarkDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Mark(
        [FromRoute] int id,
        [FromBody] MarkHabitRequest request,
        [FromServices] MarkHabitCommand command,
        [FromServices] ILogger<MarkHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.HabitCompletions.MarkName);
        var result = await command.Handle(request with { HabitId = id, UserId = user.GetId() });
        logger.LogEndpointComplete(Constants.HabitCompletions.MarkName, result);
        return result.ToApiResult();
    }
}
