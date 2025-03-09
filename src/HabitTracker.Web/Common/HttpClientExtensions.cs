using D20Tek.Functional;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HabitTracker.Web.Common;

internal static class HttpClientExtensions
{
    public static async Task<Result<T>> TryGetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, T defaultValue)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => await httpClient.GetFromJsonAsync<T>(requestUri) ?? defaultValue,
            $"{typeof(T).Name}.Get");

    public static async Task<Result<TResponse>> TryPostAsJsonAsync<TRequest, TResponse>(
        this HttpClient httpClient,
        string requestUri,
        TRequest value)
        where TRequest: notnull
        where TResponse: notnull =>
        await TrySendMessageAsync<TResponse>(
            async () => await httpClient.PostAsJsonAsync<TRequest>(requestUri, value)
                                        .MapMessageToResponse<TResponse>(),
            $"{typeof(TRequest).Name}.Post");

    private static async Task<Result<TResponse>> TrySendMessageAsync<TResponse>(
        Func<Task<Result<TResponse>>> operation,
        [CallerMemberName] string errorCode = Constants.DefaultErrorCode)
        where TResponse : notnull
    {
        try
        {
            return await operation();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            return Result<TResponse>.Failure(exception);
        }
        catch (Exception)
        {
            return Result<TResponse>.Failure(Constants.UnexpectedServiceError(errorCode));
        }
    }

    private static async Task<TResponse> MapMessageToResponse<TResponse>(this Task<HttpResponseMessage> messageTask)
        where TResponse : notnull =>
        (await (await messageTask)
            .EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<TResponse>())!;
}
