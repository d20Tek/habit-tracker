namespace HabitTracker.Api.Features.Habits;

internal static class UpdateHabitEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPut(Constants.Habits.ServiceBaseWithId, Update)
              .WithTags(nameof(Habit))
              .WithName(Constants.Habits.UpdateName)
              .WithDescription(Constants.Habits.UpdateDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateHabitRequest request,
        [FromServices] UpdateHabitCommand command,
        [FromServices] ILogger<UpdateHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.UpdateName);
        var result = await command.Handle(request.AppendUserId(user.GetId()));
        logger.LogEndpointComplete(Constants.Habits.UpdateName, result);
        return result.ToApiResult();
    }
}
