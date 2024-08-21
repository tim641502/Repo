public static class ResultFactory<T>
{
    public static Result<T> Success(T data)
    {
        return new Result<T>(data, true, null);
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T>(default, false, message);
    }
}