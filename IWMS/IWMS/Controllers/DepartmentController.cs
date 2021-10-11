using BusinessLogic.Dtos.Department;
using BusinessLogic.Services.Interfaces;
using IWMS.Configurations.Authorization;
using IWMS.Dtos.Department;
using IWMS.Helpers;
using IWMS.Mappers.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("api/department/getall")]
        public async Task<IActionResult> GetAll()
        {
            var dl = await _departmentService.GetAllAsync();

            if (dl == null)
                return NoContent();

            return Ok(dl.ToDepartmentApiListModel<DepartmentApiDto>());
        }

        [HttpGet("api/department/get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var d = await _departmentService.GetByIdAsync(id);

            if (d == null)
                return NoContent();

            return Ok(d);
        }

        [HttpPost("api/department")]
        public async Task<IActionResult> Add([FromBody]DepartmentApiDto dep)
        {
            if (await _departmentService.ExistAsync(dep.ToDepartmentApiModel<DepartmentDto>()))
                return NoContent();

            return Ok(await _departmentService.AddAsync(dep.ToDepartmentApiModel<DepartmentDto>()));
        }

        [HttpDelete("api/department/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _departmentService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("api/department")]
        public async Task<IActionResult> Update([FromBody]DepartmentApiDto dep)
        {
            if (await _departmentService.GetByIdAsync(dep.DepartmentId) == null)
                return NotFound();

            await _departmentService.UpdateAsync(dep.ToDepartmentApiModel<DepartmentDto>());

            return Ok();
        }

        [HttpPost("api/department/join")]
        public async Task<IActionResult> JoinWithUser([FromBody]EmployeeDepartmentApiDto ed)
        {
            var model = await _departmentService.JoinDepartmentWithUser(ed.ToDepartmentApiModel<EmployeeDepartmentDto>());
            if (model != null)
                return Ok(model);

            return BadRequest();
        }

        [HttpPost("api/department/leave")]
        public async Task<IActionResult> LeaveWithUser([FromBody]EmployeeDepartmentApiDto ed)
        {
            if (await _departmentService.LeaveDepartmentWithUser(ed.ToDepartmentApiModel<EmployeeDepartmentDto>()))
                return Ok();

            return BadRequest();
        }


        [HttpGet("api/department/testAut")]
        [DepartmentPermission(Department = DepartmentType.UNCATEGORY)]
        public string TestAut()
        {
            return "Ok";
        }

        [HttpGet("api/department/testPermission")]
        //[CRUDPermission(PermissionType = PermissionType.READ)]
        public string TestPermission()
        {
            return HttpContext.GetUserId();
        }

        [HttpGet("api/department/testDepartmenPermission")]
        [DepartmentPermission(Department = DepartmentType.UNCATEGORY)]
        [CRUDPermission(PermissionType = PermissionType.READ)]
        public string TestDepartmenPermission()
        {
            return DepartmentType.DEPARTMENT.ToString() +"."+ PermissionType.CREATE.ToString();
        }

        [HttpGet("api/department/testRole")]
        [DepartmentPermission(Department = DepartmentType.UNCATEGORY)]
        [CRUDPermission(PermissionType = PermissionType.READ)]
        [Authorize(Roles = "Executive")]
        public string TesRole()
        {
            return DepartmentType.DEPARTMENT.ToString() + "." + PermissionType.CREATE.ToString();
        }
    }
}
