using System.ComponentModel.DataAnnotations;

namespace Task_ProjectManagementAPI.Application.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role {  get; set; }
    }
}
