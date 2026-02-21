using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class UpdateProjectDto
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
