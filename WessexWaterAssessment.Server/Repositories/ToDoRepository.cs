public class ToDoRepository : IToDoRepository
{
    private readonly IToDoClient _toDoClient;

    public ToDoRepository(IToDoClient toDoClient)
    {
        _toDoClient = toDoClient;
    }

    public async Task<Result<ToDoSummary>> GetSummary()
    {
        var response = await _toDoClient.GetAllAsync();

        if (response.Success)
        {
            var toDos = response.Data;

            return ResultFactory<ToDoSummary>.Success(new ToDoSummary()
            {
                TotalTodos = toDos.Count(),
                CompletedTodos = toDos.Where(a => a.Completed).Count(),
                UncompletedTodos = toDos.Where(a => !a.Completed).Count()
            });
        }
        else
        {
            return ResultFactory<ToDoSummary>.Failure(response.Message);
        }
    }
}