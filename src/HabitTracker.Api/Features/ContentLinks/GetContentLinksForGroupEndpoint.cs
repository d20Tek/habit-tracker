namespace HabitTracker.Api.Features.ContentLinks;

internal static class GetContentLinksForGroupEndpoint
{
    public static WebApplication MapEndpoint(WebApplication routes)
    {
        routes.MapGet(Constants.ContentLinks.ServiceBase, GetAllForGroup)
              .WithTags(nameof(ContentLink))
              .WithName(Constants.ContentLinks.GetAllName)
              .WithDescription(Constants.ContentLinks.GetAllDesc)
              .Produces<ContentLinkResponse[]>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> GetAllForGroup(
        [FromRoute] string group,
        [FromServices] GetContentLinksForGroupCommand command,
        [FromServices] ILogger<GetContentLinksForGroupCommand> logger,
        ClaimsPrincipal user)
    {
        logger.LogEndpointStart(Constants.ContentLinks.GetAllName);
        var result = await command.Handle(new(group));
        logger.LogEndpointComplete(Constants.ContentLinks.GetAllName, result);
        return result.ToApiResult();
    }
}
