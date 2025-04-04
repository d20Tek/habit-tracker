namespace HabitTracker.Api.Persistence;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        var connectionString = builder.Configuration.GetConnectionString(Constants.DbConnectionName);

        // Register the DbContext with SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        return builder;
    }
}
