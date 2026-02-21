using System.ComponentModel.DataAnnotations;
using Test_Api.Data.Models;

namespace Test_Api.DTOs
{
    public class CreateTaskDto
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
