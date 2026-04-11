using Task_ProjectManagementAPI.Data.Models;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto?> GetByIdAsync(int taskId, string userId);
        Task<PagedResult<TaskDto>> GetByProjectAsync(int projectId, string userId , TaskParams parameters);
        Task<TaskDto> CreateAsync(CreateTaskDto dto, string userId);
        Task<bool> UpdateAsync(UpdateTaskDto dto, string userId);
        Task<bool> UpdateStatusAsync(UpdateTaskStatusDto dto, string userId);
        Task<bool> DeleteAsync(int taskId, string userId);
    }
}
