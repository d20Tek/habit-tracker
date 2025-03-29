namespace HabitTracker.Api.Persistence;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Register the DbContext with SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.EnableRetryOnFailure(2)));

        return builder;
    }
}
