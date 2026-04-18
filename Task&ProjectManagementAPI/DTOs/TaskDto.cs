using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using Test_Api.Data.Models;
using TaskStatus = Test_Api.Data.Models.TaskStatus;

namespace Test_Api.DTOs
{
    public class TaskDto
    {
        [JsonIgnore]
        public int Id { get; set; }
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
