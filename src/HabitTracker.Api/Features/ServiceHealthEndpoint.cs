namespace HabitTracker.Api.Features;

public static class ServiceHealthEndpoint
{
    public static WebApplication MapEndpoint(this WebApplication routes)
    {
        routes.MapGet(Constants.ServiceHealth.ServiceBase, () => Constants.ServiceHealth.SuccessResult)
              .WithTags(Constants.ServiceHealth.ServiceHealthType)
              .WithName(Constants.ServiceHealth.GetHealthName)
              .WithDescription(Constants.ServiceHealth.GetHealthDesc)
              .WithOpenApi();

        return routes;
    }
}
