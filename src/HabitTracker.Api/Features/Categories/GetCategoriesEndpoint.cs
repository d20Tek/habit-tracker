namespace HabitTracker.Api.Features.Categories;

public static class GetCategoriesEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Categories.ServiceBase, GetAll)
              .WithTags(nameof(Category))
              .WithName(Constants.Categories.GetAllName)
              .WithDescription(Constants.Categories.GetAllDesc)
              .Produces<CategoryResponse[]>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> GetAll(
        [FromServices] GetCategoriesForUserCommand command,
        [FromServices] ILogger<GetCategoriesForUserCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.GetAllName);
        var result = await command.Handle(user.GetId());
        logger.LogEndpointComplete(Constants.Categories.GetAllName, result.LogDetails());
        return result.ToApiResult();
    }
}
