
using Microsoft.AspNetCore.Mvc;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;

namespace Task_ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(TokenRequestModel model)
        {
            var result = await _authService.GetTokenAsync(model);
            return Ok(result);
        }

    }
}
