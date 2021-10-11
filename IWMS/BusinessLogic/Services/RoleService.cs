using BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Entities;

namespace BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<MasterAccount> _userManager;

        public RoleService(RoleManager<IdentityRole<int>> roleManager,
            UserManager<MasterAccount> userManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<(IdentityResult identityResult, int roleId)> CreateRoleAsync(IdentityRole<int> role)
        {
            var identityResult = await _roleManager.CreateAsync(role);

            return (identityResult, role.Id);
        }

        public async Task<IdentityResult> CreateUserRoleAsync(int userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole<int> role)
        {
            var thisRole = await _roleManager.FindByIdAsync(role.Id.ToString());

            return await _roleManager.DeleteAsync(thisRole);
        }

        public async Task<IdentityResult> DeleteUserRoleAsync(int userId, int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.RemoveFromRoleAsync(user, role.Name);
        }

        public async Task<bool> ExistsRoleAsync(string name)
        {
            return await _roleManager.Roles.AnyAsync(x => x.Name.Equals(name));
        }

        public async Task<IdentityRole<int>> GetRoleAsync(int roleId)
        {
            return await _roleManager.Roles.Where(x => x.Id.Equals(roleId)).SingleOrDefaultAsync();
        }

        public async Task<IdentityRole<int>> GetRolebyNameAsync(string role)
        {
            return await _roleManager.Roles.Where(x => x.Name.ToLower() == role.ToLower()).SingleOrDefaultAsync();
        }

        public async Task<List<IdentityRole<int>>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<IdentityRole<int>>> GetUserRolesAsync(string userId)
        {
            return await _roleManager.Roles.Where(x => x.Id == Convert.ToInt16(userId)).ToListAsync();
        }

        public async Task<(IdentityResult identityResult, int roleId)> UpdateRoleAsync(IdentityRole<int> role)
        {
            var thisRole = await _roleManager.FindByIdAsync(role.Id.ToString());
            thisRole.Name = role.Name;
            var identityResult = await _roleManager.UpdateAsync(thisRole);

            return (identityResult, role.Id);
        }
    }
}
