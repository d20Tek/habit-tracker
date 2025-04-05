using Blazored.SessionStorage;

namespace HabitTracker.Web.Common;

internal static class SessionStorageExtensions
{
    public static async Task<T> GetOrCreateAsync<T>(
        this ISessionStorageService sessionStorage,
        string key,
        Func<Task<T>> factory,
        TimeSpan? cacheDuration = null)
    {

        var cached = await sessionStorage.GetItemAsync<T>(key);
        var hasExpired = await sessionStorage.HasCacheExpired(key);
        return (cached is not null && hasExpired is false) ? 
            cached : 
            await CreateAsync(sessionStorage, key, factory, cacheDuration);
    }

    private static async Task<T> CreateAsync<T>(
        this ISessionStorageService sessionStorage,
        string key,
        Func<Task<T>> factory,
        TimeSpan? cacheDuration = null)
    {
        var result = await factory();
        await sessionStorage.SetItemAsync(key, result);
        await sessionStorage.SetCacheExpiration(key, cacheDuration);

        return result;
    }

    private static async Task SetCacheExpiration(
        this ISessionStorageService sessionStorage,
        string key,
        TimeSpan? cacheDuration = null)
    {
        if (cacheDuration is not null)
        {
            await sessionStorage.SetItemAsync<DateTimeOffset>(
                key + Constants.ContentLinks.ExpirationKey,
                DateTimeOffset.Now.Add(cacheDuration.Value));
        }
    }

    private static async Task<bool> HasCacheExpired(
        this ISessionStorageService sessionStorage,
        string key)
    {
        var expiration = await sessionStorage.GetItemAsync<DateTimeOffset?>(
            key + Constants.ContentLinks.ExpirationKey);

        return (expiration is null) ? false : DateTimeOffset.Now > expiration;
    }
}
