using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTaskById(Guid idTask)
        {
            var task = await _service.GetTask(idTask);
            return Ok(task);
        }

        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(Guid idTask)
        {
            await _service.DeleteTask(idTask);
            return Ok();
        }

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(TaskDTO taskDto)
        {
            await _service.UpdateTask(taskDto);
            return Ok();
        }
    }
}
