public interface IToDoRepository
{
    public Task<Result<ToDoSummary>> GetSummary();
}