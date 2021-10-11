using BusinessLogic.Dtos.Permission;
using BusinessLogic.Services.Interfaces;
using IWMS.Configurations.Authorization;
using IWMS.Dtos.Permission;
using IWMS.Mappers.Permission;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PermissionController : Controller
    {
        private readonly IPermissionService permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [HttpGet("api/permission/getall")]
        public async Task<IActionResult> GetAll()
        {
            var pers = await permissionService.GetAll();

            if (pers == null)
                return NoContent();

            return Ok(pers.ToPermissionApiListModel<PermissionApiDto>());
        }

        [HttpGet("api/permission/get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var d = await permissionService.GetById(id);

            if (d == null)
                return NoContent();

            return Ok(d);
        }

        [HttpPost("api/permission")]
        public async Task<IActionResult> Add([FromBody] PermissionApiDto dto)
        {
            if (await permissionService.Exists(dto.PermissionName.ToUpper()))
                return BadRequest();

            dto.PermissionName = dto.PermissionName.ToUpper();
            dto.PermissionCode = dto.PermissionCode.ToUpper();
            return Ok(await permissionService.Add(dto.ToPermissionApiModel<PermissionDto>()));
        }

        [HttpDelete("api/permission/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await permissionService.GetById(id) == null)
                return NotFound();

            return Ok(await permissionService.Delete(id));
        }

        [HttpPut("api/permission/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] PermissionApiDto dto)
        {
            if (await permissionService.GetById(id) == null)
                return NotFound();
            
            await permissionService.Update(dto.ToPermissionApiModel<PermissionDto>());
            return Ok();
        }

        [HttpPost("api/permission/assignPermission")]
        public async Task<IActionResult> AssignPermission([FromBody] EmployeePermissionApiDto dto)
        {
            if (await permissionService.CanAssignOrRemovePermission(dto.ToPermissionApiModel<EmployeePermissionDto>())) 
                return BadRequest();

            return Ok(await permissionService.AssignEmployeeAuthorizationAsync(dto.ToPermissionApiModel<EmployeePermissionDto>()));
        }

        [HttpDelete("api/permission/removePermission")]
        public async Task<IActionResult> RemovePermission([FromBody] EmployeePermissionApiDto dto)
        {
            if (!await permissionService.CanAssignOrRemovePermission(dto.ToPermissionApiModel<EmployeePermissionDto>()))
                return BadRequest();

            return Ok(await permissionService.RemoveEmployeeAuthorizationAsync(dto.ToPermissionApiModel<EmployeePermissionDto>()));
        }
    }
}
