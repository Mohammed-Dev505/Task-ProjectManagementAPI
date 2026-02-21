using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService) => _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string userId)
        {
            var projects = await _projectService.GetAllAsync(userId);
            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string userId)
        {
            var project = await _projectService.GetByIdAsync(id, userId);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string userId, [FromBody] CreateProjectDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var project = await _projectService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new {id = project.Id, userId }, project);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string userId, [FromBody] UpdateProjectDto dto)
        {
            if (id != dto.Id) return BadRequest("Id is not match");
            var update = await _projectService.UpdateAsync(dto, userId);
            if (!update) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string userId)
        {
            var deleted = await _projectService.DeleteAsync(id, userId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
