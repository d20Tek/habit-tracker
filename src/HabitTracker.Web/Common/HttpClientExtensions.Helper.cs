using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HabitTracker.Web.Common;

internal static partial class HttpClientExtensions
{
    private static async Task<Result<TResponse>> TrySendMessageAsync<TResponse>(
        Func<Task<Result<TResponse>>> operation,
        ILogger logger,
        [CallerMemberName] string errorCode = Constants.DefaultErrorCode)
        where TResponse : notnull
    {
        try
        {
            logger.LogInformation($"Making service request: {errorCode}");
            var result = await operation();
            logger.LogInformation("Service request result: {msg}", result.ToString());
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
            return Result<TResponse>.Failure(ex);
        }
        catch (Exception ex)
        {
            logger.LogError("Error: unexpected exception failure - {ex}", ex);
            return Result<TResponse>.Failure(Constants.UnexpectedServiceError(errorCode));
        }
    }

    private static async Task<TResponse> MapMessageToResponse<TResponse>(this Task<HttpResponseMessage> messageTask)
        where TResponse : notnull =>
        (await (await messageTask)
            .EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<TResponse>())!;
}
