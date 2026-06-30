

using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
