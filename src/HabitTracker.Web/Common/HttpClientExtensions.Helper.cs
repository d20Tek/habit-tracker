using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HabitTracker.Web.Common;

internal static partial class HttpClientExtensions
{
    private const int _testSleepDelay = 0;  // 1500;

    private static async Task<Result<TResponse>> TrySendMessageAsync<TResponse>(
        Func<Task<HttpResponseMessage>> operation,
        ILogger logger,
        [CallerMemberName] string errorCode = Constants.DefaultErrorCode)
        where TResponse : notnull
    {
        try
        {
            logger.LogInformation($"Making service request: {errorCode}");

            var httpMessage = await operation();
            var result = await MapMessageToResponse<TResponse>(httpMessage);
            logger.LogInformation("Service request result: {msg}", result.ToString());

            await SimulateDelay();
            return result;
        }
        catch (AccessTokenNotAvailableException exception)
        {
            logger.LogError("Error: access token not available - {ex}", exception);
            exception.Redirect();
            return Result<TResponse>.Failure(exception);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError("Error: http request failure - {ex}", ex);
            return Result<TResponse>.Failure(Constants.ServiceRequestError(errorCode));
        }
        catch (Exception ex)
        {
            logger.LogError("Error: unexpected exception failure - {ex}", ex);
            return Result<TResponse>.Failure(Constants.UnexpectedServiceError(errorCode));
        }
    }

    private static async Task<Result<TResponse>> MapMessageToResponse<TResponse>(this HttpResponseMessage message)
        where TResponse : notnull
    {
        if (message.IsSuccessStatusCode)
        {
            return (await message.Content.ReadFromJsonAsync<TResponse>())!;
        }
        else
        {
            var problem = await message.Content.ReadFromJsonAsync<ProblemDetails>();
            return Result<TResponse>.Failure(problem!.ToErrors());
        }
    }

    private static async Task SimulateDelay()
    {
        if (_testSleepDelay > 0)
        {
            await Task.Delay(_testSleepDelay);
        }
    }
}
