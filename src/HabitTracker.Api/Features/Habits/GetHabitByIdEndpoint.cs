using Microsoft.OpenApi.Models;

namespace HabitTracker.Api.Features.Habits;

internal static class GetHabitByIdEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Habits.ServiceBaseWithId, Get)
              .WithTags(nameof(Habit))
              .WithName(Constants.Habits.GetByIdName)
              .WithDescription(Constants.Habits.GetByIdDesc)
              .Produces<HabitResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi(operation =>
              {
                  operation.Parameters.Add(Constants.HabitCompletions.LimitCompletionsParameter);
                  return operation;
              });

        return routes;
    }

    private static async Task<IResult> Get(
        [FromRoute] int id,
        [FromQuery] int? limitCompletions,
        [FromServices] GetHabitByIdCommand command,
        [FromServices] ILogger<GetHabitByIdCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.GetByIdName);
        var result = await command.Handle(new(id, user.GetId(), limitCompletions ?? 1));
        logger.LogEndpointComplete(Constants.Habits.GetByIdName, result);
        return result.ToApiResult();
    }
}
