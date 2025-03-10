using System.Security.Claims;

namespace HabitTracker.Api.Features.Weather;

public static class WeatherForecastEndpoints
{
    private static readonly string[] _summaries =
        ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public static WebApplication MapWeatherEndpoints(this WebApplication routes)
    {
        routes.MapGet("/weatherforecast", (ClaimsPrincipal user) =>
            Enumerable.Range(1, 10).Select(index =>
                new WeatherForecast(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)])).ToArray())
        .WithName("GetWeatherForecast")
        .RequireAuthorization();

        return routes;
    }

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
