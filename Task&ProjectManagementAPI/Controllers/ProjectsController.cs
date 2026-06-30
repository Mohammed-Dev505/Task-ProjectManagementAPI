using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;

namespace Task_ProjectManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService) => _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProjectParams parameters)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _projectService.GetAllAsync(userId, parameters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _projectService.GetByIdAsync(id, userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] CreateProjectDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var project = await _projectService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = project.Id, userId }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id ,  [FromBody] UpdateProjectDto dto)
        {
            var userId = User.FindFirstValue("uid");
            await _projectService.UpdateAsync(id , dto, userId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue("uid");
            await _projectService.DeleteAsync(id,userId);
            return NoContent();
        }
    }
}
