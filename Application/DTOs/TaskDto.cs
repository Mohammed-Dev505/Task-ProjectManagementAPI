

using Domain;
using System.Text.Json.Serialization;
using TaskStatus = Domain.TaskStatus;

namespace Task_ProjectManagementAPI.Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public string ProjectName { get; set; }

        public string CreatedByUserName { get; set; }
    }
}
