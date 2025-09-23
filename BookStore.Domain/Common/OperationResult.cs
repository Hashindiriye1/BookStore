namespace BookStore.Domain.Common;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public static OperationResult<T> Success(T data, string message = "Success")
    {
        return new OperationResult<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static OperationResult<T> Failure(string message, List<string>? errors = null)
    {
        return new OperationResult<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();

    public static OperationResult Success(string message = "Success")
    {
        return new OperationResult
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static OperationResult Failure(string message, List<string>? errors = null)
    {
        return new OperationResult
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
} 