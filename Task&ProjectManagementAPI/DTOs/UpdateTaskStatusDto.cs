using System.ComponentModel.DataAnnotations;
using TaskStatus = Test_Api.Data.Models.TaskStatus;

namespace Test_Api.DTOs
{
    public class UpdateTaskStatusDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public TaskStatus Status { get; set; }
    }
}
