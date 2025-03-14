namespace HabitTracker.Api.Features.Habits;

internal static class GetHabitsEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Habits.ServiceBase, GetAll)
              .WithTags(nameof(Habit))
              .WithName(Constants.Habits.GetAllName)
              .WithDescription(Constants.Habits.GetAllDesc)
              .Produces<HabitResponse[]>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> GetAll(
        [FromServices] GetHabitsCommand command,
        [FromServices] ILogger<GetHabitsCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.GetAllName);
        var result = await command.Handle(user.GetId());
        logger.LogEndpointComplete(Constants.Habits.GetAllName, result);
        return result.ToApiResult();
    }
}
