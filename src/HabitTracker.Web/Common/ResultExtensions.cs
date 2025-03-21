namespace HabitTracker.Web.Common;

internal static class ResultExtensions
{
    internal static void HandleResult<T>(this Result<T> result, Action<T> onSuccess, Action<string> onFailure)
        where T : notnull
    {
        if (result.IsSuccess)
            onSuccess(result.GetValue());
        else
            onFailure(result.GetErrors().First().ToString());
    }

    internal static async Task HandleResultAsync<T>(
        this Task<Result<T>> result, Action<T> onSuccess, Action<string> onFailure)
        where T : notnull
    {
        var r = await result;
        if (r.IsSuccess)
            onSuccess(r.GetValue());
        else
            onFailure(r.GetErrors().First().ToString());
    }

    internal static async Task HandleResultAsync<T>(
        this Task<Result<T>> result, Action<T> onSuccess, Action<Error[]> onFailure)
        where T : notnull
    {
        var r = await result;
        if (r.IsSuccess)
            onSuccess(r.GetValue());
        else
            onFailure(r.GetErrors());
    }

    internal static async Task<T> HandleErrorAsync<T>(
        this Task<Result<T>> result, Action<string> onFailure, T defaultValue)
        where T : notnull
    {
        var r = await result;
        if (r.IsSuccess)
        {
            return r.GetValue();
        }
        else
        {
            onFailure(r.GetErrors().First().ToString());
            return defaultValue;
        }
    }

    internal static void MatchAction<T>(this Option<T> option, Action<T> onSome, Action onNone)
        where T : notnull
    {
        if (option.IsSome)
            onSome(option.Get());
        else
            onNone();
    }

    internal static async Task MatchActionAsync<T>(this Task<Option<T>> option, Action<T> onSome, Action onNone)
        where T : notnull
    {
        var opt = await option;
        if (opt.IsSome)
            onSome(opt.Get());
        else
            onNone();
    }
}
