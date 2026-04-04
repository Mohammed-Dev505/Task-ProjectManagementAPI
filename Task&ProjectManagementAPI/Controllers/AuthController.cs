using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Services.Interfaces;

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
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticted)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(TokenRequestModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.GetTokenAsync(model);
            if(!result.IsAuthenticted)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole(AddRoleModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.AddRoleAsync(model);
            if(!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok("Role assigned success");
        }
    }
}
