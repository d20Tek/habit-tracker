using Blazored.SessionStorage;

namespace HabitTracker.Web.Common;

internal static class SessionStorageExtensions
{
    public static async Task<T> GetOrCreateAsync<T>(
        this ISessionStorageService sessionStorage,
        string key,
        Func<Task<T>> factory)
    {
        var cached = await sessionStorage.GetItemAsync<T>(key);
        return (cached is not null) ? 
            cached : 
            await CreateAsync(sessionStorage, key, factory);
    }

    private static async Task<T> CreateAsync<T>(
        this ISessionStorageService sessionStorage,
        string key,
        Func<Task<T>> factory)
    {
        var result = await factory();
        await sessionStorage.SetItemAsync(key, result);
        return result;
    }
}
