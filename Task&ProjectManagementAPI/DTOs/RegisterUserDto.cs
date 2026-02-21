using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class RegisterUserDto
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
