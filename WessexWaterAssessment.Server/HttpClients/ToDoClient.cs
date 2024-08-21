using System.Text;
using Newtonsoft.Json;

public class ToDoClient : IToDoClient
{
    private readonly HttpClient _httpClient;

    public ToDoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<IEnumerable<ToDo>>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                var toDos = JsonConvert.DeserializeObject<IEnumerable<ToDo>>(content);

                if (toDos == null)
                {
                    return ResultFactory<IEnumerable<ToDo>>.Failure($"{_httpClient.BaseAddress} get request returned success code but null data");
                }

                return ResultFactory<IEnumerable<ToDo>>.Success(toDos);
            }
            else
            {
                return ResultFactory<IEnumerable<ToDo>>.Failure($"Failed getting all todos from {_httpClient.BaseAddress}, error code {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return ResultFactory<IEnumerable<ToDo>>.Failure(ex.Message);
        }
    }
}