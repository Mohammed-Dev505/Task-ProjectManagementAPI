using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test_Api.Data.Models;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccoutsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AccoutsController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Created(string.Empty, _mapper.Map<UserDto>(user));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null) return Unauthorized("Invalid Username or password ");
            var isPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPassword) return Unauthorized("Invalid Username or password ");
            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
