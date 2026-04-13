using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
