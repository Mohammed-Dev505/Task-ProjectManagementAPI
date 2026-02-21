using System.Text.Json.Serialization;

namespace Test_Api.DTOs
{
    public class ProjectDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TasksCount { get; set; }
    }
}
