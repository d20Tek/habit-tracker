using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Persistence;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Register the DbContext with SQL Server
        builder.Services.AddDbContext<HabitTrackerDbContext>(options =>
            options.UseSqlServer(connectionString));

        return builder;
    }
}
