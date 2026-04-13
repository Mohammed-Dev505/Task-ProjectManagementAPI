using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
