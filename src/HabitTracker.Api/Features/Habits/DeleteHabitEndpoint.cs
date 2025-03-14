namespace HabitTracker.Api.Features.Habits;

internal static class DeleteHabitEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapDelete(Constants.Habits.ServiceBaseWithId, Delete)
              .WithTags(nameof(Habit))
              .WithName(Constants.Habits.DeleteName)
              .WithDescription(Constants.Habits.DeleteDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Delete(
        [FromRoute] int id,
        [FromServices] DeleteHabitCommand command,
        [FromServices] ILogger<DeleteHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.DeleteName);
        var result = await command.Handle(new(id, user.GetId()));
        logger.LogEndpointComplete(Constants.Habits.DeleteName, result);
        return result.ToApiResult();
    }
}
