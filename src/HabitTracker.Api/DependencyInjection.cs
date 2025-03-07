using HabitTracker.Api.Features.Categories;

namespace HabitTracker.Api;

internal static class DependencyInjection
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseCors();

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        CategoryEndpoints.MapCategoryEndpoints(app);

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 10).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        return app;
    }

    private static string[] _summaries =
        [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" ];

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
