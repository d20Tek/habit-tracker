namespace HabitTracker.Api.Features.Habits;

internal static class CreateHabitEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPost(Constants.Habits.ServiceBase, Create)
              .WithTags(nameof(Habit))
              .WithName(Constants.Habits.CreateName)
              .WithDescription(Constants.Habits.CreateDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Create(
        [FromBody] CreateHabitRequest request,
        [FromServices] CreateHabitCommand command,
        [FromServices] ILogger<CreateHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.CreateName);
        var result = await command.Handle(request.AppendUserId(user.GetId()));
        logger.LogEndpointComplete(Constants.Habits.CreateName, result.ToString());

        var catId = result.Match(c => c.Id, _ => 0);
        return result.ToCreatedApiResult($"{Constants.Habits.ServiceBase}/{catId}");
    }
}
