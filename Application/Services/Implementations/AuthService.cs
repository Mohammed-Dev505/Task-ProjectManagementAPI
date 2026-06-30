
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_ProjectManagementAPI.Application.Exceptions;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;
using Task_ProjectManagementAPI.Domain.Entities;

namespace Task_ProjectManagementAPI.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<User> userManager , RoleManager<IdentityRole> roleManager , IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<bool> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
                throw new NotFoundException($"User with ID {model.UserId} not found");

            if (!await _roleManager.RoleExistsAsync(model.Role))
                throw new BadRequestException($"Role {model.Role} does not exist");

            if (await _userManager.IsInRoleAsync(user, model.Role))
                throw new BadRequestException($"User already assigned to this role");

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong while assigning role");

            return true;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { Message = "Email or Password is incorrect" };

            var JwtToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                IsAuthenticted = true,
                ExpireOn = JwtToken.ValidTo,
                Roles = roles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken)
            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registerd" };
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user , model.Password);
            if(!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            var JwtToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                IsAuthenticted = true,
                ExpireOn = JwtToken.ValidTo,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken)
            };
        }

        public async Task<bool> RemoveRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
                throw new NotFoundException($"User with ID {model.UserId} not found");

            if (!await _roleManager.RoleExistsAsync(model.Role))
                throw new BadRequestException($"Role {model.Role} does not exist");

            if (!await _userManager.IsInRoleAsync(user, model.Role))
                throw new BadRequestException("User does not have thie role to remove");

            var result = await _userManager.RemoveFromRoleAsync(user, model.Role);
            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong while removing role");

            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName , user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email , user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentails = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var JwtToken = new JwtSecurityToken
                (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentails
                );

            return JwtToken;
        }
    }
}
