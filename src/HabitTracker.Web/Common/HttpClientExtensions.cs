using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HabitTracker.Web.Common;

internal static class HttpClientExtensions
{
    public static async Task<Result<T>> TryGetFromJsonAsync<T>(
        this HttpClient httpClient,
        string requestUri,
        T defaultValue,
        ILogger logger)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => await httpClient.GetFromJsonAsync<T>(requestUri) ?? defaultValue,
            logger,
            $"{typeof(T).Name}.Get");

    public static async Task<Result<T>> TryGetByIdFromJsonAsync<T>(
        this HttpClient httpClient,
        string requestUri,
        ILogger logger)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => (await httpClient.GetFromJsonAsync<T>(requestUri))!,
            logger,
            $"{typeof(T).Name}.GetById");

    public static async Task<Result<TResponse>> TryPostAsJsonAsync<TRequest, TResponse>(
        this HttpClient httpClient,
        string requestUri,
        TRequest value,
        ILogger logger)
        where TRequest : notnull
        where TResponse : notnull =>
        await TrySendMessageAsync<TResponse>(
            async () => await httpClient.PostAsJsonAsync<TRequest>(requestUri, value)
                                        .MapMessageToResponse<TResponse>(),
            logger,
            $"{typeof(TRequest).Name}.Post");

    public static async Task<Result<TResponse>> TryPutAsJsonAsync<TRequest, TResponse>(
        this HttpClient httpClient,
        string requestUri,
        TRequest value,
        ILogger logger)
        where TRequest : notnull
        where TResponse : notnull =>
        await TrySendMessageAsync<TResponse>(
            async () => await httpClient.PutAsJsonAsync<TRequest>(requestUri, value)
                                        .MapMessageToResponse<TResponse>(),
            logger,
            $"{typeof(TRequest).Name}.Put");

    public static async Task<Result<T>> TryDeleteAsJsonAsync<T>(
        this HttpClient httpClient,
        string requestUri,
        ILogger logger)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => await httpClient.DeleteAsync(requestUri)
                                        .MapMessageToResponse<T>(),
            logger,
            $"{typeof(T).Name}.Delete");

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
