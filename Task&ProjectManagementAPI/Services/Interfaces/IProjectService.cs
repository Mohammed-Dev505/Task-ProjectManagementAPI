using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync(string userId);
        Task<ProjectDto?> GetByIdAsync(int projectId, string userId);
        Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId);
        Task<bool> UpdateAsync(UpdateProjectDto dto, string userId);
        Task<bool> DeleteAsync(int projectId, string userId);
    }
}
