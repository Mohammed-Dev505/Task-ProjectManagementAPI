

using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Application.Models;

namespace Task_ProjectManagementAPI.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto?> GetByIdAsync(Guid taskId, string userId);
        Task<PagedResult<TaskDto>> GetByProjectAsync(Guid projectId, string userId , TaskParams parameters);
        Task<TaskDto> CreateAsync(Guid projectId  , CreateTaskDto dto, string userId);
        Task<bool> UpdateAsync(Guid id , UpdateTaskDto dto, string userId);
        Task<bool> UpdateStatusAsync( Guid id , UpdateTaskStatusDto dto, string userId);
        Task<bool> DeleteAsync(Guid taskId, string userId);
    }
}
