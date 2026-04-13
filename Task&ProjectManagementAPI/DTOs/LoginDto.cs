using System.ComponentModel.DataAnnotations;

namespace Test_Api.DTOs
{
    public class LoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
