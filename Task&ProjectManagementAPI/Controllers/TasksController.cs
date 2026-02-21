using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService) => _taskService = taskService;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string userId)
        {
            var task = await _taskService.GetByIdAsync(id, userId);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetByProject(int projectId, [FromQuery] string userId)
        {
            var tasks = await _taskService.GetByProjectAsync(projectId, userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string userId, CreateTaskDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var task = await _taskService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = task.Id ,  userId }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string userId, [FromBody] UpdateTaskDto dto)
        {
            if (id != dto.Id) return BadRequest("Id is not match");
            var update = await _taskService.UpdateAsync(dto, userId);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string userId, [FromBody] UpdateTaskStatusDto dto)
        {
            if (id != dto.Id) return BadRequest("Id is not match");
            var update = await _taskService.UpdateStatusAsync(dto, userId);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string userId)
        {
            var delete = await _taskService.DeleteAsync(id, userId);
            if (!delete) return NotFound();
            return NoContent();
        }
    }
}
