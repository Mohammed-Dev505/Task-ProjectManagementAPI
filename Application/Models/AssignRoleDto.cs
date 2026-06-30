

using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class AssignRoleDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
