using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController:Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet("api/role")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await roleService.GetRolesAsync());
        }

        [HttpGet("api/role/{id}")]
        public async Task<ActionResult> GetRole([FromRoute]int id)
        {
            var role = await roleService.GetRoleAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost("api/role")]
        public async Task<ActionResult> CreateRole([FromBody]IdentityRole<int> role)
        {
            role.ConcurrencyStamp = null;
            var (identity, id) = await roleService.CreateRoleAsync(role);

            if (identity.Succeeded)
                return Ok();

            return BadRequest();
        }

        [HttpPut("api/role/{id}")]
        public async Task<ActionResult> UpdateRole([FromRoute]int id,[FromBody]IdentityRole<int> role)
        {
            var check = await roleService.GetRoleAsync(id);
            if (check == null)
                return NotFound();

            var (identity, roleId) = await roleService.UpdateRoleAsync(role);

            if (identity.Succeeded)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("api/role/{id}")]
        public async Task<ActionResult> DeleteRole([FromRoute]int id)
        {
            var role = await roleService.GetRoleAsync(id);
            if (role == null)
                return NotFound();

            await roleService.DeleteRoleAsync(role);

            return NoContent();
        }

        [HttpPost("api/role/assignRole/{id}/{roleId}")]
        public async Task<ActionResult> AssignUserRole([FromRoute]int id,[FromRoute]string name)
        {
            var role = await roleService.GetRolebyNameAsync(name);
            if (role == null)
                return NotFound();

            var result = await roleService.CreateUserRoleAsync(id, role.Name);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("api/role/removeRole/{id}/{roleId}")]
        public async Task<ActionResult> RemoveUserRole([FromRoute]int id, [FromRoute]int roleId)
        {
            var role = await roleService.GetRoleAsync(id);
            if (role == null)
                return NotFound();

            var result = await roleService.DeleteUserRoleAsync(id, roleId);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

    }
}
