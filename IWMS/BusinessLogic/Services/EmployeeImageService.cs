using BusinessLogic.Dtos.Employee;
using BusinessLogic.Helpers;
using BusinessLogic.Mapper;
using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmployeeImageService:IEmployeeImageService
    {
        private readonly IAsyncRepository<EmployeeImage> _repository;
        private readonly IImageWriter _imageWriter;

        public EmployeeImageService(IAsyncRepository<EmployeeImage> repository, IImageWriter imageWriter)
        {
            _repository = repository;
            _imageWriter = imageWriter;
        }

        public async Task<EmployeeImageDto> AddAsync(EmployeeImageDto model,int empId)
        {
            var result = await _imageWriter.UploadImage(model.EmployeeImage);
            result.EmployeeId = empId;

            var image = await _repository.AddAsync(result.ToEmployeeImageEntityDto<EmployeeImage>());
            return image.ToEmployeeImageEntityDto<EmployeeImageDto>();
        }

        public async Task DeleteAsync(int empId)
        {
            var image = await GetByEmployeeId(empId);
            _imageWriter.DeleteImage(image.ImageName);
            await _repository.DeleteAsync(image.ToEmployeeImageEntityDto<EmployeeImage>());
        }

        public async Task<bool> ExistsAsync(int EmployeeId)
        {
            return await _repository.ExistsAsync(x => x.EmployeeId == EmployeeId);
        }

        public async Task<EmployeeImageDto> GetByEmployeeId(int EmployeeId)
        {
            var image = await _repository.GetSingleAsync(x => x.EmployeeId == EmployeeId);
            return image.ToEmployeeImageEntityDto<EmployeeImageDto>();
        }

        public async Task<EmployeeImageDto> UpdateAsync(EmployeeImageDto model, int empId)
        {
            var entity = await _repository.GetSingleAsync(x => x.EmployeeId == empId);

            //delete original image
            _imageWriter.DeleteImage(entity.ImageName);

            var result = await _imageWriter.UploadImage(model.EmployeeImage);
            entity.ImageBinary = result.ImageBinary;
            entity.ImageHeader = result.ImageHeader;
            entity.ImageName = result.ImageName;

            await _repository.UpdateAsync(entity);
            return entity.ToEmployeeImageEntityDto<EmployeeImageDto>();
        }
    }
}
