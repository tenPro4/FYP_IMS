using BusinessLogic.Dtos.Employee;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Common;
using IWMS.Configurations.Authorization;
using IWMS.Dtos;
using IWMS.Dtos.Employee;
using IWMS.Helpers;
using IWMS.Mappers.Employee;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController:Controller
    {
        private readonly IAuthenService authenService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeImageService _employeeImageService;
        private readonly IEmployeeAddressService employeeAddressService;

        public EmployeeController(IEmployeeService employeeService,
            IEmployeeImageService employeeImageService,
            IAuthenService authenService,
            IEmployeeAddressService employeeAddressService)
        {
            _employeeService = employeeService;
            _employeeImageService = employeeImageService;
            this.authenService = authenService;
            this.employeeAddressService = employeeAddressService;

        }

        //[CRUDPermission(PermissionType = PermissionType.Create)]
        [HttpPost("api/employee/post")]
        public async Task<IActionResult> Add([FromBody]EmployeeApiDto viewModel)
        {
            var employee = new EmployeeDto
            {
                EmployeeId = viewModel.EmployeeId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Gender = viewModel.Gender,
                CardNo = viewModel.CardNo,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                ChangedDate = DateTime.Now,
                AccountId = viewModel.AccountId
            };

            await _employeeService.AddAsync(employee);

            return Ok(employee);
        }

        [HttpPut("api/employee/update/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody]EmployeeApiDto viewModel)
        {
            if (await _employeeService.ExistsAsync(id))
                return NotFound();

            var employee = new EmployeeDto
            {
                EmployeeId = viewModel.EmployeeId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Gender = viewModel.Gender,
                PhoneNumber = viewModel.PhoneNumber,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                ChangedDate = DateTime.Now,
            };

            await _employeeService.AddAsync(employee);

            return Ok(employee);
        }

        [HttpGet("api/employee/getAll")]
        public async Task<IActionResult> GetAll()
        {
            var employees =await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpPost("api/employee/updatePermission")]
        public async Task<IActionResult> UpdatePermission([FromBody]CommonRequest req)
        {
            var result = await _employeeService.UpdateEmployeePermission(req);

            return Ok(result);
        }

        [HttpGet("api/employee/{id}/notProjectUser")]
        public async Task<IActionResult> GetNoProjectUsers([FromRoute]int id)
        {
            var employees = await _employeeService.GetNoProjectMemberAsync(id);

            return Ok(employees);
        }

        [HttpGet("api/employee/get/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var employees = await _employeeService.GetByEmployeeIdAsync(id);

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("api/employee/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var empId = HttpContext.GetEmpId();
            var employees = await _employeeService.GetByEmployeeIdAsync(Int32.Parse(empId));

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        [HttpPatch("api/employee/profile/{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute]int id,[FromBody]EmployeeApiDto employee)
        {
            var employees = await _employeeService.GetByEmployeeIdAsync(id);

            if (employees == null)
                return NotFound();

            await _employeeService.UpdateAsync(employee.ToEmployeeApiModel<EmployeeDto>(),id);

            return Ok();
        }

        [HttpPost("api/employeeImage/postImage")]
        public async Task<IActionResult> AddImage(IFormFile image)
        {
            var empId = Int32.Parse(HttpContext.GetEmpId());

            if (image != null)
            {
                var employeeImage = new EmployeeImageDto
                {
                    EmployeeImage = image
                };

                var existingImage = await _employeeImageService.ExistsAsync(empId);

                if (existingImage)
                    employeeImage = await _employeeImageService.UpdateAsync(employeeImage, empId);
                else
                    employeeImage = await _employeeImageService.AddAsync(employeeImage, empId);

                return Ok(employeeImage);
            }

            return NoContent();
        }

        [HttpDelete("api/employeeImage/removeImage/{empId}")]
        public async Task<IActionResult> RemoveImage([FromRoute]int empId)
        {
           if(await _employeeImageService.ExistsAsync(empId))
            {
                await _employeeImageService.DeleteAsync(empId);
            }

            return NoContent();
        }

        [HttpGet("api/employee/address/{empId}")]
        public async Task<IActionResult> AddressGetById(int empId)
        {
            var address = await employeeAddressService.GetByEmployeeId(empId);
            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost("api/employeeImage/address/{empId}")]
        public async Task<IActionResult> AddAddress([FromRoute]int empId, [FromBody]EmployeeAddressApiDto dto)
        {
            var address = await employeeAddressService.AddAsync(dto.ToEmployeeApiModel<EmployeeAddressDto>());

            return Ok(address);
        }

        [HttpPatch("api/employeeImage/address/{empId}")]
        public async Task<IActionResult> UpdateAddress([FromRoute]int empId, [FromBody]EmployeeAddressApiDto dto)
        {
            await employeeAddressService.UpdateAsync(empId,dto.ToEmployeeApiModel<EmployeeAddressDto>());

            return Ok();
        }

    }
}
