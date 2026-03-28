using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasksByUserId(Guid idUser)
        {
            var tasks = await _service.GetTasksByUserId(idUser);
            return Ok(tasks);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(TaskDTO taskDto, Guid idUser)
        {
            await _service.CreateTask(taskDto, idUser);
            return Ok();
        }
    }
}
