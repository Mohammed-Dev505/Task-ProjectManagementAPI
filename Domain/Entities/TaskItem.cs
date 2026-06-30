using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = Domain.TaskStatus;

namespace Task_ProjectManagementAPI.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public Guid Id { get; set; }

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
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        // User who created the task
        [ForeignKey(nameof(CreatedByUser))]
        public string CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}
