
using TaskStatus = Domain.TaskStatus;

namespace Task_ProjectManagementAPI.Application.DTOs
{
    public class UpdateTaskStatusDto
    {
        public TaskStatus Status { get; set; }
    }
}
