using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test_Api.Data.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProjectStatus { Active, Completed, Archived }
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; } 
        public ProjectStatus Status { get; set; } = ProjectStatus.Active;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // User who created the project (Identity)
        [ForeignKey(nameof(CreatedByUser))]
        public string CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }

        // Navigation: One Project → Many Tasks
        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
