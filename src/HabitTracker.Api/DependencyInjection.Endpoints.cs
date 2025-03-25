using HabitTracker.Api.Features.Categories;
using HabitTracker.Api.Features.ContentLinks;
using HabitTracker.Api.Features.Habits;
using HabitTracker.Api.Features.Weather;
using HabitTracker.Api.Features.Weighings;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HabitTracker.Api;

internal static partial class DependencyInjection
{
    public static WebApplication MapEndpoints(this WebApplication app) =>
        app.MapEndpointFunc(GetCategoriesEndpoint.MapEndpoint)
           .MapEndpointFunc(GetCategoryByIdEndpoint.MapEndpoint)
           .MapEndpointFunc(CreateCategoryEndpoint.MapEndpoint)
           .MapEndpointFunc(UpdateCategoryEndpoint.MapEndpoint)
           .MapEndpointFunc(DeleteCategoryEndpoint.MapEndpoint)

           .MapEndpointFunc(GetHabitsEndpoint.MapEndpoint)
           .MapEndpointFunc(GetHabitByIdEndpoint.MapEndpoint)
           .MapEndpointFunc(CreateHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(UpdateHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(DeleteHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(MarkHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(UnmarkHabitEndpoint.MapEndpoint)

           .MapEndpointFunc(GetWeighingsForUserEndpoint.MapEndpoint)
           .MapEndpointFunc(GetWeighingByIdEndpoint.MapEndpoint)
           .MapEndpointFunc(UpsertWeighingEndpoint.MapEndpoint)
           .MapEndpointFunc(DeleteWeighingEndpoint.MapEndpoint)

           .MapEndpointFunc(GetContentLinksForGroupEndpoint.MapEndpoint)

           .MapHeathCheckEndpoint()
           .MapWeatherEndpoints();

    private static WebApplication MapHeathCheckEndpoint(this WebApplication app)
    {
        app.MapHealthChecks(
            Constants.ServiceHealth.ServiceBase,
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });

        return app;
    }
}
