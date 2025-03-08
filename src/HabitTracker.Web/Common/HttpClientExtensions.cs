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
}
