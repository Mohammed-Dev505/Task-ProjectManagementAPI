using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.Data.Models;

namespace Task_ProjectManagementAPI.Services.Implementations
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

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to the role";
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user is null || !await _userManager.CheckPasswordAsync(user,model.Password))
            {
                authModel.Message = "Email or Password is Incurrect";
                return authModel;
            }
            var jwtToken = await CreateJWtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            authModel.IsAuthenticted = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.Roles = roles.ToList();
            authModel.Username = user.UserName;
            authModel.ExpireOn = jwtToken.ValidTo;
            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };
            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered" };
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var JwtToken = await CreateJWtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                Username = user.UserName,
                IsAuthenticted = true,
                ExpireOn = JwtToken.ValidTo,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken)
            };
        }
        private async Task<JwtSecurityToken> CreateJWtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim("roles", r)).ToList();
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }.Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken
                (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }
    }
}
