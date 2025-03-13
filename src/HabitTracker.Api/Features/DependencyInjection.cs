using HabitTracker.Api.Features.Categories;
using HabitTracker.Api.Features.Habits;

namespace HabitTracker.Api.Features;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddCategoryCommands();

        return builder;
    }

    private static IServiceCollection AddCategoryCommands(this IServiceCollection services) =>
        services.AddScoped<GetCategoriesForUserCommand>()
                .AddScoped<GetCategoryByIdCommand>()
                .AddScoped<CreateCategoryCommand>()
                .AddScoped<UpdateCategoryCommand>()
                .AddScoped<DeleteCategoryCommand>()

                .AddScoped<GetHabitsCommand>()
                .AddScoped<GetHabitByIdCommand>()
                .AddScoped<CreateHabitCommand>()
                .AddScoped<UpdateHabitCommand>()
                .AddScoped<DeleteHabitCommand>();
}
