namespace HabitTracker.Api.Common;

public static class TryCatch
{
    public static async Task<T> RunAsync<T>(Func<Task<T>> operation, Func<Exception, T> onException, Action? onFinally = null)
        where T : notnull
    {
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            return onException(e);
        }
        finally
        {
            onFinally?.Invoke();
        }
    }
}