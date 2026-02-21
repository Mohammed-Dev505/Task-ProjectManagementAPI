using System.ComponentModel.DataAnnotations;
using Test_Api.Data.Models;

namespace Test_Api.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
