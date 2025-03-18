using Microsoft.OpenApi.Models;

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
              .WithOpenApi(operation =>
              {
                  operation.Parameters.Add(new OpenApiParameter
                  {
                      Name = "limitCompletions",
                      In = ParameterLocation.Query,
                      Required = false,
                      Schema = new OpenApiSchema { Type = "integer", Format = "int32" },
                      Description = "Optional limit on number of DailyCompletions returned with the Habit."
                  });

                  return operation;
              });

        return routes;
    }

    private static async Task<IResult> Unmark(
        [FromRoute] int id,
        [FromQuery] int? limitCompletions,
        [FromBody] UnmarkHabitRequest request,
        [FromServices] UnmarkHabitCommand command,
        [FromServices] ILogger<UnmarkHabitCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.HabitCompletions.UnmarkName);
        var result = await command.Handle(request with { HabitId = id, UserId = user.GetId() }, limitCompletions ?? 1);
        logger.LogEndpointComplete(Constants.HabitCompletions.UnmarkName, result);
        return result.ToApiResult();
    }
}
