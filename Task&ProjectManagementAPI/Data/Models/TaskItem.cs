using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Api.Data.Models
{
    public enum TaskStatus
    {
        Todo = 0,
        InProgress = 1,
        Done = 2,
        Blocked = 3
    }

    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Todo;

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        // FK → Project
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        // User who created the task
        [ForeignKey(nameof(CreatedByUser))]
        public string CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}
