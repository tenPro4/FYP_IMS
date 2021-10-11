using BusinessLogic.Services.Interfaces;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using EntityFramework.Entities;
using System.Threading.Tasks;
using BusinessLogic.Dtos.Project;
using BusinessLogic.Mapper;
using EntityFramework.Specifications;
using System.Linq;
using BusinessLogicShared.Request;
using BusinessLogicShared.ExceptionHandling;
using BusinessLogicShared.Common;
using BusinessLogicShared.Constants;
using BusinessLogic.Dtos.Employee;

namespace BusinessLogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IAsyncRepository<MasterProject> projectRepository;
        private readonly IRoleService roleService;
        private readonly IAsyncRepository<ProjectUser> projectUserRep;
        private readonly IAsyncRepository<TaskUser> taskUserRep;
        private readonly IAsyncRepository<Employee> empRep;
        private readonly IAsyncRepository<ProjectColumn> projectColumnRep;
        private readonly IAsyncRepository<MasterTask> taskRep;
        private readonly IAsyncRepository<TaskComment> commentRep;

        public ProjectService(
            IAsyncRepository<MasterProject> projectRepository, 
            IRoleService roleService,
            IAsyncRepository<ProjectUser> projectUserRep,
            IAsyncRepository<ProjectColumn> projectColumnRep,
            IAsyncRepository<MasterTask> taskRep,
            IAsyncRepository<Employee> empRep,
            IAsyncRepository<TaskUser> taskUserRep,
            IAsyncRepository<TaskComment> commentRep
            )
        {
            this.projectRepository = projectRepository;
            this.roleService = roleService;
            this.projectUserRep = projectUserRep;
            this.projectColumnRep = projectColumnRep;
            this.taskRep = taskRep;
            this.empRep = empRep;
            this.taskUserRep = taskUserRep;
            this.commentRep = commentRep;
        }

        public async Task<ProjectColumnDto> AddColumn(int id)
        {
            var columns = await projectColumnRep.GetAsync(x => x.ProjectId == id);
            var lastOrder = columns.Count > 0 ? columns.Max(x => x.Order) + 1 : 1;

            var newColumn = new ProjectColumn
            {
                Title = "New Column",
                Order = lastOrder,
                ProjectId = id
            };

            var result = await projectColumnRep.AddAsync(newColumn);

            return result.ToProjectModel<ProjectColumnDto>();
        }

        public async Task<List<ProjectUserDto>> AddProjectMembersAsync(int id, ProjectUserRequest req)
        {
            if (req.Members == null)
                throw new Exception("Empty");

            var newMembers = new List<ProjectUser>();

            foreach(var member in req.Members)
            {
                var spec = new EmpSpecification(x => x.EmployeeId == member);
                newMembers.Add(new ProjectUser
                {
                    EmployeeId = member,
                    Employee = await empRep.GetSingleAsync(spec),
                    ProjectId = id
                });
            }

            var result = await projectUserRep.AddRangeAsync(newMembers);

            return result.ToProjectListModel<ProjectUserDto>();
        }

        public async Task DeleteColumn(int id)
        {
            var exists = await projectColumnRep.GetSingleAsync(x => x.Id == id);

            if (exists == null)
                throw new Exception("No Exist");

            await projectColumnRep.DeleteAsync(exists);
        }

        public virtual async Task<bool> DeleteProjectAsync(int id)
        {
            if (!await ExistsAsync(id))
                return false;

            var entity = await projectRepository.GetByIdAsync(id);
            
            return await projectRepository.DeleteAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await projectRepository.ExistsAsync(x => x.ProjectId == id);
        }

        public virtual async Task<ProjectDto> GetProjectAsync(int id)
        {
            var spec = new ProjectSpecification(x => x.ProjectId == id);
            var data = await projectRepository.GetSingleAsync(spec);
            var pu = new ProjectUserSpecification(x => x.ProjectId == id);
            data.ProjectUser = await projectUserRep.GetAsync(pu);
            var model = data.ToProjectModel<ProjectDto>();

            var colSpec = new ProjectColumnSpecification(x => x.ProjectId == id);
            var columns = await projectColumnRep.GetAsync(colSpec,x => x.Order);
            var columnsModel = columns.ToProjectListModel<ProjectColumnDto>();

            foreach (var col in columnsModel)
            {
                var taskSpec = new TaskSpecification(x => x.ColumnId == col.Id);
                var tasks = await taskRep.GetAsync(taskSpec,x => x.ListPosition);
                foreach(var task in tasks)
                {
                    var empSpec = new TaskUserSpecification(x => x.TaskId == task.TaskId);
                    task.Assignees = await taskUserRep.GetAsync(empSpec);
                    task.TaskComment = task.TaskComment.OrderByDescending(x => x.DateCreated).ToList();
                }
                col.MasterTask = tasks.ToProjectListModel<TaskDto>();
            }

            model.Column = columnsModel;

            return model;
        }

        public virtual async Task<List<ProjectDto>> GetProjectsAsync()
        {
            var spec = new ProjectSpecification();
            var data = await projectRepository.GetWithoutFilterAsync(spec);

            return data.ToProjectListModel<ProjectDto>();
        }

        public virtual async Task<ProjectOverview> ProjectOverviewAsync(int employeeId)
        {
            var spec = new ProjectSpecification();
            var data = await projectRepository.GetWithoutFilterAsync(spec);

            var projectOverview = new ProjectOverview();

            projectOverview.TotalProjectActive = data.Where(x => x.Status == ProjectStatus.Active).Count();
            projectOverview.TotalProjecArchived = data.Where(x => x.Status == ProjectStatus.Archived).Count();
            projectOverview.TotalProjectPause = data.Where(x => x.Status == ProjectStatus.Pause).Count();
            projectOverview.TotalProjectSuspended = data.Where(x => x.Status == ProjectStatus.Suspended).Count();

            foreach (var project in data)
            {
                var p = await GetProjectAsync(project.ProjectId);
                foreach (var column in p.Column)
                {
                    projectOverview.TotalTaskBug += column.MasterTask.Where(x => x.TaskType == TaskType.Bug).Count();
                    projectOverview.TotalTaskDesign += column.MasterTask.Where(x => x.TaskType == TaskType.Design).Count();
                    projectOverview.TotalTaskEnchancement += column.MasterTask.Where(x => x.TaskType == TaskType.Enhancement).Count();
                    projectOverview.TotalTaskReview += column.MasterTask.Where(x => x.TaskType == TaskType.Review).Count();

                    foreach (var task in column.MasterTask)
                    {
                        if (task.Assignees.Where(x => x.EmployeeId == employeeId).Count() > 0)
                        {
                            switch (task.TaskPriority)
                            {
                                case TaskPriority.HIGH:
                                    projectOverview.HightTask += 1;
                                    break;
                                case TaskPriority.MEDIUM:
                                    projectOverview.MediumTask += 1;
                                    break;
                                case TaskPriority.LOW:
                                    projectOverview.LowTask += 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return projectOverview;
        }

        public virtual async Task<ProjectDto> InsertProjectAsync(AddProjectRequest req)
        {
            var entity = new MasterProject
            {
                Name = req.Name,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Description = req.Description,
                EmployeeLeaderId = req.EmployeeLeaderId,
                Status = ProjectStatus.Active,
            };

            if (req.ProjectUser != null)
            {
                foreach (var id in req.ProjectUser)
                {
                    entity.ProjectUser.Add(new ProjectUser { EmployeeId = id });
                }
            }

            entity.ProjectUser.Add(new ProjectUser { EmployeeId = req.EmployeeLeaderId });

            var result =await projectRepository.AddAsync(entity);

            return result.ToProjectModel<ProjectDto>();
        }

        public virtual async Task<ProjectUserDto> JoinProjectAsync(ProjectUserDto dto)
        {
            try
            {
                var entity = dto.ToProjectModel<ProjectUser>();
                var result = await projectUserRep.AddAsync(entity);

                return result.ToProjectModel<ProjectUserDto>();

            }catch(Exception e)
            {
                throw new UserFriendlyErrorPageException(e.Message);
            }
        }

        public virtual async Task LeaveProjectAsync(ProjectUserDto dto)
        {
            try
            {
                var entity = await projectUserRep.GetSingleAsync(x => x.ProjectId == dto.ProjectId && x.EmployeeId == dto.EmployeeId);
                await projectUserRep.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new UserFriendlyErrorPageException(e.Message);
            }
        }

        public async Task UpdateProjectAsync(int id, ProjectDto dto)
        {
            await projectRepository.UpdateAsync(dto.ToProjectModel<MasterProject>());
        }

        public async Task UpdateProjectColumnOrderAsync(int id, ProjectColumnDto dto)
        {
            var allColumns = await projectColumnRep.GetAsync(x => x.ProjectId == id);

            var startIndex = 1;

            foreach(var colId in dto.Ids)
            {
                var column = allColumns.Where(x => x.Id == colId).FirstOrDefault();
                if(column.Order != startIndex)
                {
                    column.Order = startIndex;
                    await projectColumnRep.UpdateAsync(column);
                }

                startIndex += 1;
            }
        }

        public async Task UpdateProjectColumnTitleAsync(int id, ProjectColumnDto dto)
        {
            var entity = await projectColumnRep.GetSingleAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Column no exists");

            entity.Title = dto.Title;

            await projectColumnRep.UpdateAsync(entity);
        }
    }
}
