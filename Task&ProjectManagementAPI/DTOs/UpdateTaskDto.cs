using System.ComponentModel.DataAnnotations;
using Test_Api.Data.Models;

namespace Test_Api.DTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
