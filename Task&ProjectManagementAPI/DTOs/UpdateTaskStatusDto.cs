using System.ComponentModel.DataAnnotations;
using TaskStatus = Test_Api.Data.Models.TaskStatus;

namespace Test_Api.DTOs
{
    public class UpdateTaskStatusDto
    {
        public int Id { get; set; }

        public TaskStatus Status { get; set; }
    }
}
