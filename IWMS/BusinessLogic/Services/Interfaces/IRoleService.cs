using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> ExistsRoleAsync(string name);
        Task<List<IdentityRole<int>>> GetRolesAsync();
        Task<IdentityRole<int>> GetRoleAsync(int roleId);
        Task<IdentityRole<int>> GetRolebyNameAsync(string role);
        Task<(IdentityResult identityResult, int roleId)> CreateRoleAsync(IdentityRole<int> role);
        Task<(IdentityResult identityResult, int roleId)> UpdateRoleAsync(IdentityRole<int> role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole<int> role);
        Task<IdentityResult> CreateUserRoleAsync(int userId, string roleId);
        Task<List<IdentityRole<int>>> GetUserRolesAsync(string userId);
        Task<IdentityResult> DeleteUserRoleAsync(int userId, int roleId);

        //Task<IdentityResult> CreateRoleClaimsAsync(IdentityRoleClaim<int> claims);
        //Task<IdentityResult> DeleteeRoleClaimsAsync(IdentityRoleClaim<int> claims);
    }
}
