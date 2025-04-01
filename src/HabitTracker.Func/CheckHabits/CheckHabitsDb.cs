using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitTracker.Func.CheckHabits;

public class CheckHabitsDb
{
    private readonly HabitsDbContext _dbContext;
    private readonly ILogger _logger;

    public CheckHabitsDb(HabitsDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        _logger = loggerFactory.CreateLogger<CheckHabitsDb>();
    }

    [Function("CheckHabitsDb")]
    public async Task Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"Azure Function executed at: {DateTime.Now}");

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next timer scheduled for: {myTimer.ScheduleStatus.Next}");
        }

        try
        {
            // Query the database
            var links = await _dbContext.ContentLinks.ToListAsync();

            if (links.Any())
            {
                _logger.LogInformation($"Retrieved {links.Count} links from the HabitsDatabase.");
            }
            else
            {
                _logger.LogError($"Database retrieval failed: No links returned.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"HabitsDb Exception Failure: {ex.Message}");
            throw;
        }
    }
}
