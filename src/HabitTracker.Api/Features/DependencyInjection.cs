using HabitTracker.Api.Features.Categories;

namespace HabitTracker.Api.Features;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<GetCategoriesForUserCommand>();

        return builder;
    }
}
