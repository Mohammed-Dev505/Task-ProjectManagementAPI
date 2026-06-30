

using Task_ProjectManagementAPI.Application.Models;

namespace Task_ProjectManagementAPI.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<bool> AddRoleAsync(AddRoleModel model);
        Task<bool> RemoveRoleAsync(AddRoleModel model); 
    }
}
