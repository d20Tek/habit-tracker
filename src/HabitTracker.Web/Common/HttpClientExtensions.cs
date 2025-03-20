using System.Net.Http.Json;

namespace HabitTracker.Web.Common;

internal static partial class HttpClientExtensions
{
    public static async Task<Result<T>> TryGetFromJsonAsync<T>(
        this HttpClient http,
        string requestUri,
        T defaultValue,
        ILogger logger)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => await http.GetFromJsonAsync<T>(requestUri) ?? defaultValue, logger, $"{typeof(T).Name}.Get");

    public static async Task<Result<T>> TryGetByIdFromJsonAsync<T>(
        this HttpClient http,
        string requestUri,
        ILogger logger)
        where T : notnull =>
        await TrySendMessageAsync<T>(
            async () => (await http.GetFromJsonAsync<T>(requestUri))!, logger, $"{typeof(T).Name}.GetById");

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
}
