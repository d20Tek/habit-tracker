using Microsoft.OpenApi.Models;

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
              .WithOpenApi(operation =>
              {
                  operation.Parameters.Add(new OpenApiParameter
                  {
                      Name = "limitCompletions",
                      In = ParameterLocation.Query,
                      Required = false,
                      Schema = new OpenApiSchema { Type = "integer", Format = "int32" },
                      Description = "Optional limit on number of DailyCompletions retrieved per Habit."
                  });

                  return operation;
              });

        return routes;
    }

    private static async Task<IResult> GetAll(
        [FromQuery] int? limitCompletions,
        [FromServices] GetHabitsCommand command,
        [FromServices] ILogger<GetHabitsCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Habits.GetAllName);
        var result = await command.Handle(user.GetId(), limitCompletions ?? 1);
        logger.LogEndpointComplete(Constants.Habits.GetAllName, result);
        return result.ToApiResult();
    }
}
