using HabitTracker.Api.Features.Categories;
using HabitTracker.Api.Features.ContentLinks;
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
                        .AddWeighingCommands()
                        .AddContentLinksCommands()
                        .AddHealthChecks()
                            .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Default");

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
        services.AddScoped<GetWeighingsForUserCommand>()
                .AddScoped<GetWeighingByIdCommand>()
                .AddScoped<UpsertWeighingCommand>()
                .AddScoped<DeleteWeighingCommand>();

    private static IServiceCollection AddContentLinksCommands(this IServiceCollection services) =>
        services.AddScoped<GetContentLinksForGroupCommand>();
}
