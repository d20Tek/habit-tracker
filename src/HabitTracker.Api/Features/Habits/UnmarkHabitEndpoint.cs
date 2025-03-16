namespace HabitTracker.Api.Features.Habits;

internal static class UnmarkHabitEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPut(Constants.HabitCompletions.UnmarkServiceBase, Unmark)
              .WithTags(nameof(Habit))
              .WithName(Constants.HabitCompletions.UnmarkName)
              .WithDescription(Constants.HabitCompletions.UnmarkDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Unmark(
        [FromRoute] int id,
        [FromBody] UnmarkHabitRequest request,
        [FromServices] UnmarkHabitCommand command,
        [FromServices] ILogger<UnmarkHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.HabitCompletions.UnmarkName);
        var result = await command.Handle(request with { HabitId = id, UserId = user.GetId() });
        logger.LogEndpointComplete(Constants.HabitCompletions.UnmarkName, result);
        return result.ToApiResult();
    }
}
