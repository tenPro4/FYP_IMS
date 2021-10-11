using BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Leave;
using System.Threading.Tasks;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using BusinessLogic.Mapper;
using BusinessLogic.Helpers;
using EntityFramework.Specifications;
using BusinessLogicShared.Common;
using BusinessLogic.Dtos.Enums;
using Microsoft.AspNetCore.Http;
using BusinessLogicShared.ExceptionHandling;

namespace BusinessLogic.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IAsyncRepository<Leave> leaveRepository;
        private readonly IAsyncRepository<SupportFile> fileRepository;
        private readonly IFileWriter fileWriter;
        private readonly IEmployeeService empService;

        public LeaveService(IAsyncRepository<Leave> leaveRepository,
            IFileWriter fileWriter,
            IAsyncRepository<SupportFile> fileRepository,
            IEmployeeService empService)
        {
            this.leaveRepository = leaveRepository;
            this.fileWriter = fileWriter;
            this.fileRepository = fileRepository;
            this.empService = empService;
        }

        public virtual async Task<LeaveDto> AddAsync(LeaveDto leave)
        {
            var sf = new SupportFile();
            if(leave.Upload != null)
            {
                leave.SupportFile = await fileWriter.UploadDocument(leave.Upload);
                sf = await fileRepository.AddAsync(leave.SupportFile.ToLeaveModel<SupportFile>());
                leave.SupportFileId = sf.Id;
            }

            var entity = await leaveRepository.AddAsync(leave.ToLeaveModel<Leave>());

            var result = entity.ToLeaveModel<LeaveDto>();
            result.Employee = await empService.GetByEmployeeIdAsync(result.EmployeeId);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            if(await ExistsAsync(id))
            {
                var entity = await leaveRepository.GetSingleAsync(x => x.Id == id);
                await leaveRepository.DeleteAsync(entity);
            }
                
        }

        public async Task DeleteLeavesAsync(CommonRequest req)
        {
            foreach(var leave in req.Leaves)
            {
                var entity = await leaveRepository.GetByIdAsync(leave);
                await leaveRepository.DeleteAsync(entity);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await leaveRepository.ExistsAsync(x => x.Id == id);
        }

        public virtual async Task<List<LeaveDto>> GetLeavesAsync()
        {
            var spec = new LeaveSpecification();
            var leaves = await leaveRepository.GetWithoutFilterAsync(spec);

            return leaves.ToLeaveListModel<LeaveDto>();
        }

        public virtual List<SelectItem> GetLeaveType()
        {
            return EnumHelpers.ToSelectList<LeaveTypes>();
        }

        public virtual async Task<LeaveDto> GetSingleAsync(int id)
        {
            var spec = new LeaveSpecification(x => x.Id == id);
            var entity = await leaveRepository.GetSingleAsync(spec);
            var model = entity.ToLeaveModel<LeaveDto>();

            if (entity.SupportFileId.HasValue)
            {
                model.FileDownloadLink = fileWriter.DownloadFiles(entity.SupportFile.FileName);
            }

            return model;
        }

        public virtual async Task<List<LeaveDto>> GetUnApproveLeaveAsync()
        {
            var entity = await leaveRepository.GetAsync(x => x.Approved == false);

            return entity.ToLeaveListModel<LeaveDto>();
        }

        public virtual async Task<bool> LeaveApproval(int id)
        {
            if (!await ExistsAsync(id))
                return false;

            var entity = await leaveRepository.GetByIdAsync(id);
            entity.Approved = true;

            return await leaveRepository.UpdateAsync(entity);
        }

        public virtual async Task<LeaveDto> UpdateAsync(int id,LeaveDto leave)
        {
            if(await ExistsAsync(id))
            {
                var spec = new LeaveSpecification(x => x.Id == id);
                var entity = await leaveRepository.GetSingleAsync(spec);
                entity.LeaveType = leave.LeaveType;
                entity.Approved = leave.Approved;
                entity.Start = leave.Start;
                entity.End = leave.End;
                entity.Description = leave.Description;

                await leaveRepository.UpdateAsync(entity);

                return entity.ToLeaveModel<LeaveDto>();
            }

            throw new UserFriendlyErrorPageException("Id not exists");
        }

        public async Task<LeaveDto> UploadFile(int id,IFormFile file)
        {
            var spec = new LeaveSpecification(x => x.Id == id);
            var entity = await leaveRepository.GetSingleAsync(spec);

            //delete original before add new file
            //if (entity.SupportFile != null)
            //{
            //    fileWriter.DeleteDocument(entity.SupportFile.FileName);
            //    var supportModel = await fileWriter.UploadDocument(file);
            //    entity.SupportFile = supportModel.ToLeaveModel<SupportFile>();
            //    var sf = await fileRepository.AddAsync(entity.SupportFile);
            //    leave.SupportFileId = sf.Id;
            //}

            var supportModel = await fileWriter.UploadDocument(file);
            entity.SupportFile = supportModel.ToLeaveModel<SupportFile>();
            var sf = await fileRepository.AddAsync(entity.SupportFile);
            entity.SupportFileId = sf.Id;

            await leaveRepository.UpdateAsync(entity);

            return entity.ToLeaveModel<LeaveDto>();
        }
    }
}
