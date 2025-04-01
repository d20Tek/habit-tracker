using HabitTracker.Func.CheckHabits;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HabitTracker.Func;

internal static class DependencyInjection
{
    private const string _habitsDbConnection = "HabitsDbConnection";
    private const string _habitsServiceUrl = "ServiceUrls:HabitsService";
    private const string _habitsServiceName = "HabitsService";

    public static IServiceCollection ConfigureHabitsRequirements(
        this IServiceCollection services,
        HostBuilderContext host)
    {
        string connectionString = host.Configuration.GetConnectionString(_habitsDbConnection)
                                      ?? throw new InvalidOperationException("Habits SQL connection string is missing.");

        services.AddDbContext<HabitsDbContext>(options => options.UseSqlServer(connectionString));

        string serviceUrl = host.Configuration[_habitsServiceUrl]
                                ?? throw new InvalidOperationException("Habits SQL connection string is missing.");

        services.AddHttpClient(_habitsServiceName, client => client.BaseAddress = new(serviceUrl));

        return services;
    }
}
