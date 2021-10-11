using BusinessLogic.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProjectColumnService
    {
        Task<ProjectColumnDto> Add(ProjectColumnDto dto);
        Task Delete(int id);
        Task<List<ProjectColumnDto>> GetAll();
        Task<ProjectColumnDto> GetById(int id);
        Task<ProjectColumnDto> Update(ProjectColumnDto dto);
    }
}
