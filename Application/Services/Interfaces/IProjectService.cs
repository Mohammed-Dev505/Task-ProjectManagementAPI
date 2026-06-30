

using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Application.Models;

namespace Task_ProjectManagementAPI.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<PagedResult<ProjectDto>> GetAllAsync(string userId , ProjectParams parameters);
        Task<ProjectDto?> GetByIdAsync(Guid projectId, string userId);
        Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId);
        Task<bool> UpdateAsync(Guid id , UpdateProjectDto dto, string userId);
        Task<bool> DeleteAsync(Guid projectId, string userId);
    }
}
