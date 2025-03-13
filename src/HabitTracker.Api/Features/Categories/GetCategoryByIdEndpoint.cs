namespace HabitTracker.Api.Features.Categories;

internal static class GetCategoryByIdEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.Categories.ServiceBaseWithId, Get)
              .WithTags(nameof(Category))
              .WithName(Constants.Categories.GetByIdName)
              .WithDescription(Constants.Categories.GetByIdDesc)
              .Produces<CategoryResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Get(
        [FromRoute] int id,
        [FromServices] GetCategoryByIdCommand command,
        [FromServices] ILogger<GetCategoryByIdCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.GetByIdName);
        var result = await command.Handle(new(id, user.GetId()));
        logger.LogEndpointComplete(Constants.Categories.GetByIdName, result.LogDetails());
        return result.ToApiResult();
    }
}
