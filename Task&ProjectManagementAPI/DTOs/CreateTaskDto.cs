using System.ComponentModel.DataAnnotations;
using Test_Api.Data.Models;

namespace Test_Api.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        public int ProjectId { get; set; }
    }
}
