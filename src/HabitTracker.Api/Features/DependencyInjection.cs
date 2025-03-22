using HabitTracker.Api.Features.Categories;
using HabitTracker.Api.Features.Habits;
using HabitTracker.Api.Features.Weighings;

namespace HabitTracker.Api.Features;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddCategoryCommands()
                        .AddHabitCommands()
                        .AddWeighingCommands();

        return builder;
    }

    private static IServiceCollection AddCategoryCommands(this IServiceCollection services) =>
        services.AddScoped<GetCategoriesForUserCommand>()
                .AddScoped<GetCategoryByIdCommand>()
                .AddScoped<CreateCategoryCommand>()
                .AddScoped<UpdateCategoryCommand>()
                .AddScoped<DeleteCategoryCommand>();

    private static IServiceCollection AddHabitCommands(this IServiceCollection services) =>
        services.AddScoped<GetHabitsCommand>()
                .AddScoped<GetHabitByIdCommand>()
                .AddScoped<CreateHabitCommand>()
                .AddScoped<UpdateHabitCommand>()
                .AddScoped<DeleteHabitCommand>()
                .AddScoped<MarkHabitCommand>()
                .AddScoped<UnmarkHabitCommand>();

    private static IServiceCollection AddWeighingCommands(this IServiceCollection services) =>
        services.AddScoped<UpsertWeighingCommand>();

}
