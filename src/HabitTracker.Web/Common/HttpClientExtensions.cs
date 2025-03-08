using D20Tek.Functional;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;

namespace HabitTracker.Web.Common;

internal static class HttpClientExtensions
{
    public static async Task<T> TryGetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, T defaultValue)
        where T : notnull
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<T>(requestUri) ?? defaultValue;
            return result;
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            return defaultValue;
        }
    }

    public static async Task<Result<TResponse>> TryPostAsJsonAsync<TRequest, TResponse>(
        this HttpClient httpClient,
        string requestUri,
        TRequest value)
        where TRequest: notnull
        where TResponse: notnull
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<TRequest>(requestUri, value);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            return result!;
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            return Result<TResponse>.Failure(exception);
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Failure(ex);
        }
    }
}
