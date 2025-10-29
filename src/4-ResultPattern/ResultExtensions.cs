namespace ResultPattern;

public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        // Properties
        public bool IsFailure => !result.IsSuccess;

        public T ValueOrDefault => result.IsSuccess ? result.Value! : default!;

        public string ErrorSummary => result.IsFailure
            ? string.Join(", ", result.Errors)
            : string.Empty;

        // Monadic operations
        public Result<TResult> Map<TResult>(Func<T, TResult> mapper)
        {
            if (result.IsFailure)
                return Result<TResult>.Failure(result.ErrorMessage ?? "Unknown error");

            try
            {
                var mapped = mapper(result.Value!);
                return Result<TResult>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<TResult>.Failure(ex.Message);
            }
        }

        public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> binder)
        {
            if (result.IsFailure)
                return Result<TResult>.Failure(result.ErrorMessage ?? "Unknown error");

            try
            {
                return binder(result.Value!);
            }
            catch (Exception ex)
            {
                return Result<TResult>.Failure(ex.Message);
            }
        }

        public Result<T> OnSuccess(Action<T> action)
        {
            if (result.IsSuccess)
                action(result.Value!);
            return result;
        }

        public Result<T> OnFailure(Action<string> action)
        {
            if (result.IsFailure)
                action(result.ErrorMessage ?? "Unknown error");
            return result;
        }

        public TResult Match<TResult>(
            Func<T, TResult> onSuccess,
            Func<string, TResult> onFailure)
            => result.IsSuccess
                ? onSuccess(result.Value!)
                : onFailure(result.ErrorMessage ?? "Unknown error");

        // Operators
        public static bool operator true(Result<T> r) => r.IsSuccess;
        public static bool operator false(Result<T> r) => !r.IsSuccess;

        public static Result<T> operator |(Result<T> first, Result<T> second)
            => first.IsSuccess ? first : second;
    }
}
