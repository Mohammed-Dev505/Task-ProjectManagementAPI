using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Application.Exceptions;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;


namespace Task_ProjectManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService) => _taskService = taskService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _taskService.GetByIdAsync(id, userId));
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(Guid projectId , [FromQuery] TaskParams parameters)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _taskService.GetByProjectAsync(projectId, userId, parameters));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid projectId ,  CreateTaskDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var task = await _taskService.CreateAsync(projectId ,dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = task.Id ,  userId }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
        {
            var userId = User.FindFirstValue("uid");
            await _taskService.UpdateAsync(id, dto, userId);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var update = await _taskService.UpdateStatusAsync( id, dto, userId);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue("uid");
            await _taskService.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
