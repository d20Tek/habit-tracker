using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HabitTracker.Func.CheckHabits
{
    public class CheckHabitsServices
    {
        private const string _serviceName = "HabitsService";
        private const string _contentLinksUrl = "/api/v1/content-links/home-sidebar";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public CheckHabitsServices(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<CheckHabitsServices>();
        }

        [Function("CheckHabitsServices")]
        public async Task Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Azure Function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer scheduled for: {myTimer.ScheduleStatus.Next}");
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient(_serviceName);
                var response = await httpClient.GetAsync(_contentLinksUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"API Response: {data}");
                }
                else
                {
                    _logger.LogError($"API call failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitsApi Exception Failure: {ex.Message}");
                throw;
            }
        }
    }
}
