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
                  operation.Parameters.Add(new OpenApiParameter
                  {
                      Name = "limitCompletions",
                      In = ParameterLocation.Query,
                      Required = false,
                      Schema = new OpenApiSchema { Type = "integer", Format = "int32" },
                      Description = "Optional limit on number of DailyCompletions retrieved for the Habit."
                  });

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
