using BusinessLogic.Dtos.Leave;
using BusinessLogicShared.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ILeaveService
    {
        Task<LeaveDto> AddAsync(LeaveDto leave);
        Task<LeaveDto> UpdateAsync(int id,LeaveDto leave);
        Task<LeaveDto> GetSingleAsync(int id);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<LeaveDto>> GetLeavesAsync();
        Task<bool> LeaveApproval(int id);
        Task<List<LeaveDto>> GetUnApproveLeaveAsync();
        List<SelectItem> GetLeaveType();
        Task<LeaveDto> UploadFile(int id,IFormFile file);
        Task DeleteLeavesAsync(CommonRequest req);

    }
}
