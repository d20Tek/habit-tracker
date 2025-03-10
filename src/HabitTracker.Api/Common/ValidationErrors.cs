using D20Tek.Functional;

namespace HabitTracker.Api.Common;

public class ValidationErrors
{
    private readonly IList<Error> _errors = [];

    public Error this[int index] => _errors[index];

    public bool HasErrors => _errors.Count > 0;

    private ValidationErrors() { }

    public static ValidationErrors Create() => new();

    public ValidationErrors AddIfError(Func<bool> check, Error error)
    {
        if (check()) _errors.Add(error);
        return this;
    }

    public ValidationErrors AddIfError(Func<bool> check, string code, string message) =>
        AddIfError(check, Error.Validation(code, message));

    public Result<T> Map<T>(Func<T> onSuccess) where T : notnull =>
         HasErrors ? Result<T>.Failure([.. _errors]) : onSuccess();
}
