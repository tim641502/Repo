using Microsoft.AspNetCore.Mvc;

namespace WessexWaterAssessment.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(ILogger<ToDoController> logger, IToDoRepository toDoRepository)
        {
            _logger = logger;
            _toDoRepository = toDoRepository;
        }

        public async Task<Result<ToDoSummary>> GetAsync()
        {
            var response = await _toDoRepository.GetSummary();

            if (!response.Success)
            {
                _logger.LogError(response.Message);
            }

            return response;
        }
    }
}
