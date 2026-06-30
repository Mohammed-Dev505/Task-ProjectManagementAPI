

using Domain;

namespace Task_ProjectManagementAPI.Application.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }
    }
}
