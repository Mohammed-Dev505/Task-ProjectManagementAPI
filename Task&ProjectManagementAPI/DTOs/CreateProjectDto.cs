using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class CreateProjectDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
