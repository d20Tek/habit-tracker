namespace HabitTracker.Api.Features.Weighings;

internal static class GetWeighingsForUserEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Weighings.ServiceBase, GetAll)
              .WithTags(nameof(Weighing))
              .WithName(Constants.Weighings.GetAllName)
              .WithDescription(Constants.Weighings.GetAllDesc)
              .Produces<WeighingResponse[]>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> GetAll(
        [FromServices] GetWeighingsForUserCommand command,
        [FromServices] ILogger<GetWeighingsForUserCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.GetAllName);
        var result = await command.Handle(user.GetId());
        logger.LogEndpointComplete(Constants.Categories.GetAllName, result);
        return result.ToApiResult();
    }
}
