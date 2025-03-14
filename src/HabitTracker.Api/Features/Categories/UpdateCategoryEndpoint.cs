namespace HabitTracker.Api.Features.Categories;

internal static class UpdateCategoryEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPut(Constants.Categories.ServiceBaseWithId, Update)
              .WithTags(nameof(Category))
              .WithName(Constants.Categories.UpdateName)
              .WithDescription(Constants.Categories.UpdateDesc)
              .Produces<CategoryResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateCategoryRequest request,
        [FromServices] UpdateCategoryCommand command,
        [FromServices] ILogger<UpdateCategoryCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.UpdateName);
        var result = await command.Handle(request.AppendUserId(user.GetId()));
        logger.LogEndpointComplete(Constants.Categories.UpdateName, result);
        return result.ToApiResult();
    }
}
