namespace ex05;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public int Attempts { get; }
    public string? Error { get; }

    public Result(bool success, string error, int attempts)
    {
        Success = success;
        Error = error;
        Attempts = attempts;
        Value = default(T);
    }
    
    public Result(bool success, T value, int attempts)
    {
        Success = success;
        Value = value;
        Attempts = attempts;
        Error = default;
    }
}