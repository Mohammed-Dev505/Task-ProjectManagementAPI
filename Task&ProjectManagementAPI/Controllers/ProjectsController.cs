using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.DTOs;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue("uid");
            return Ok(await _projectService.GetByIdAsync(id, userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] CreateProjectDto dto)
        {
            var userId = User.FindFirstValue("uid");
            var project = await _projectService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new {id = project.Id, userId }, project);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id ,  [FromBody] UpdateProjectDto dto)
        {
            if (id != dto.Id) throw new BadHttpRequestException($"Id mismatch");
            var userId = User.FindFirstValue("uid");
            await _projectService.UpdateAsync(dto, userId);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue("uid");
            await _projectService.DeleteAsync(id,userId);
            return NoContent();
        }
    }
}
