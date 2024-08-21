public class Result<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

    public Result(T data, bool success, string message)
    {
        Data = data;
        Success = success;
        Message = message;
    }   
}