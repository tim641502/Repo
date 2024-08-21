public interface IToDoClient
{
    public Task<Result<IEnumerable<ToDo>>> GetAllAsync();
}