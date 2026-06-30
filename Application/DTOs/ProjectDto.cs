using System.Text.Json.Serialization;

namespace Task_ProjectManagementAPI.Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TasksCount { get; set; }
    }
}
