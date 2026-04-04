using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_ProjectManagementAPI.Exceptions;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService) => _taskService = taskService;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _taskService.GetByIdAsync(id, userId));
        }

        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _taskService.GetByProjectAsync(projectId, userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateTaskDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var task = await _taskService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = task.Id ,  userId }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            if (id != dto.Id) throw new  BadRequestException("Id mismatch");
            var userId = User.FindFirstValue("uid");
            await _taskService.UpdateAsync(dto, userId);
            return NoContent();
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTaskStatusDto dto)
        {
            var userId = User.FindFirstValue("uid");
            if (id != dto.Id) return BadRequest("Id is not match");
            var update = await _taskService.UpdateStatusAsync(dto, userId);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue("uid");
            await _taskService.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
