namespace HabitTracker.Api.Features.Categories;

internal static class CreateCategoryEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapPost(Constants.Categories.ServiceBase, Create)
              .WithTags(nameof(Category))
              .WithName(Constants.Categories.CreateName)
              .WithDescription(Constants.Categories.UpdateDesc)
              .Produces<CategoryResponse>()
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status401Unauthorized)
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> Create(
        [FromBody] CreateCategoryRequest request,
        [FromServices] CreateCategoryCommand command,
        [FromServices] ILogger<CreateCategoryCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.Categories.CreateName);
        var result = await command.Handle(request.AppendUserId(user.GetId()));
        logger.LogEndpointComplete(Constants.Categories.CreateName, result.LogDetails());

        var catId = result.Match(c => c.Id, _ => 0);
        return result.ToCreatedApiResult($"{Constants.Categories.ServiceBase}/{catId}");
    }
}
