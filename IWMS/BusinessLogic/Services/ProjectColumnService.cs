using BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Project;
using System.Threading.Tasks;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Entities;
using BusinessLogicShared.ExceptionHandling;
using BusinessLogic.Mapper;
using EntityFramework.Specifications;

namespace BusinessLogic.Services
{
    public class ProjectColumnService : IProjectColumnService
    {
        private readonly IAsyncRepository<ProjectColumn> statusRepository;

        public ProjectColumnService(IAsyncRepository<ProjectColumn> statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public async Task<ProjectColumnDto> Add(ProjectColumnDto dto)
        {
            if(await statusRepository.ExistsAsync(x => x.Title == dto.Title))
                throw new Exception("alreadly exists");

            try
            {
                var entity = dto.ToProjectModel<ProjectColumn>();
                var result = await statusRepository.AddAsync(entity);

                return result.ToProjectModel<ProjectColumnDto>();

            }catch(Exception e)
            {
                throw new UserFriendlyErrorPageException(e.Message);
            }
        }

        public async Task Delete(int id)
        {
            if (!await statusRepository.ExistsAsync(x => x.Id == id))
                throw new Exception("no exists");

            try
            {
                var entity = await statusRepository.GetSingleAsync(x => x.Id == id);
                await statusRepository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new UserFriendlyErrorPageException(e.Message);
            }
        }

        public async Task<List<ProjectColumnDto>> GetAll()
        {
            var spec = new ProjectColumnSpecification();
            var result = await statusRepository.GetWithoutFilterAsync(spec);

            return result.ToProjectListModel<ProjectColumnDto>();
        }

        public virtual async Task<ProjectColumnDto> GetById(int id)
        {
            var spec = new ProjectColumnSpecification(x => x.Id == id);
            var result = await statusRepository.GetSingleAsync(spec);

            return result.ToProjectModel<ProjectColumnDto>();
        }

        public async Task<ProjectColumnDto> Update(ProjectColumnDto dto)
        {
            if (!await statusRepository.ExistsAsync(x => x.Id == dto.Id))
                throw new Exception("no exists");

            var entity = dto.ToProjectModel<ProjectColumn>();

            await statusRepository.UpdateAsync(entity);

            return dto;
        }
    }
}
