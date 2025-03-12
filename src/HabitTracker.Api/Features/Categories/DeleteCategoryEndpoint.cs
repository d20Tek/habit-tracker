namespace HabitTracker.Api.Features.Categories;

internal static class DeleteCategoryEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapDelete(Constants.Categories.ServiceBase + "/{id}", Delete)
              .WithTags(nameof(Category))
              .WithName(Constants.Categories.DeleteName)
              .WithDescription(Constants.Categories.DeleteDesc)
              .Produces<CategoryResponse>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Delete(
        [FromRoute] int id,
        [FromServices] DeleteCategoryCommand command,
        [FromServices] ILogger<DeleteCategoryCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.DeleteName);
        var result = await command.Handle(new(id, user.GetId()));
        logger.LogEndpointComplete(Constants.Categories.DeleteName, result.LogDetails());
        return result.ToApiResult();
    }
}
