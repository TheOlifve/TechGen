namespace ex05;

static public class Executor<T>
{
    public static Result<T> Execute<T>(Func<T> operation, int maxAttempts,
        Func<Exception, bool>? shouldRetry = null)
    {
        int attempt = 1;

        while (attempt <= maxAttempts)
        {
            try
            {
                return new Result<T>(true, operation(), attempt);
            }
            catch (Exception ex)
            {
                if (!shouldRetry?.Invoke(ex) ?? false)
                    return new Result<T>(false, ex.Message, attempt);
            }
            attempt++;
        }
        
        attempt--;
        
        return new Result<T>(false, "Maximum retry attempts exceeded", attempt);
    }
}