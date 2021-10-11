using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Project;
using System.Threading.Tasks;
using BusinessLogic.Mapper;
using EntityFramework.Specifications;
using System.Linq;
using BusinessLogic.Helpers;
using BusinessLogic.Dtos.Employee;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IAsyncRepository<TaskComment> taskCommentRepository;
        private readonly IAsyncRepository<TaskUser> taskUserRepository;
        private readonly IAsyncRepository<MasterTask> taskRepository;
        private readonly IAsyncRepository<Employee> empRepository;

        public TaskService(IAsyncRepository<TaskComment> taskCommentRepository, 
            IAsyncRepository<MasterTask> taskRepository,
            IAsyncRepository<Employee> empRepository,
            IAsyncRepository<TaskUser> taskUserRepository
            )
        {
            this.taskCommentRepository = taskCommentRepository;
            this.taskRepository = taskRepository;
            this.empRepository = empRepository;
            this.taskUserRepository = taskUserRepository;
        }

        public async Task<TaskCommentDto> AddCommentAsync(int id,TaskCommentDto comment)
        {
            comment.DateCreated = DateTime.Now;
            comment.DateUpdated = DateTime.Now;

            var spec = new EmpSpecification(x => x.EmployeeId == comment.EmployeeId);
            var empEntity = await empRepository.GetSingleAsync(spec);
            comment.Employee = empEntity.ToProjectModel<EmployeeDto>();
            comment.TaskId = id;

            var data =  await taskCommentRepository.AddAsync(comment.ToProjectModel<TaskComment>());

            return data.ToProjectModel<TaskCommentDto>();
        }

        public async Task<TaskDto> AddTaskAsync(int id,TaskDto dto)
        {
            var task = new MasterTask
            {
                Title = dto.Title,
                Description = dto.Description,
                TaskPriority = dto.TaskPriority,
                TaskType = dto.TaskType,
            };
            task.DateCreated = DateTime.Now;
            task.DateUpdated = DateTime.Now;
            task.DueDate = DateTime.Now;
            task.ColumnId = id;
            task.ListPosition = await calculateListPosition(id);

            foreach (var assign in dto.Assignees)
            {
                var spec = new EmpSpecification(x => x.EmployeeId == assign.EmployeeId);
                task.Assignees.Add(new TaskUser
                {
                    Employee = await empRepository.GetSingleAsync(spec),
                    EmployeeId = assign.EmployeeId,
                });
            }

            var data = await taskRepository.AddAsync(task);

            return data.ToProjectModel<TaskDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await taskRepository.GetByIdAsync(id);

            if (entity == null)
                return false;

            return await taskRepository.DeleteAsync(entity);
        }

        public async Task<List<TaskDto>> GetTaskBaseColumnAsync(int columnId)
        {
            var spec = new TaskSpecification(x => x.ColumnId == columnId);
            var tasks = await taskRepository.GetAsync(spec);

            return tasks.ToProjectListModel<TaskDto>();
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var spec = new TaskSpecification(x => x.TaskId == id);
            var exists = await taskRepository.GetSingleAsync(spec);
            if (exists == null)
                return null;

            var model = exists.ToProjectModel<TaskDto>();

            return model;
        }

        public async Task<TaskDto> UpdateAsync(int id,TaskDto dto)
        {
            var spec = new TaskSpecification(x => x.TaskId == id);
            var task = await taskRepository.GetSingleAsync(spec);
            task.Title = dto.Title ?? task.Title;
            task.DueDate = dto.DueDate ?? task.DueDate;
            task.Description = dto.Description ?? task.Description;
            task.DateUpdated = DateTime.UtcNow;
            task.TaskPriority = dto.TaskPriority == 0 || dto.TaskPriority.Equals(null) ? task.TaskPriority : dto.TaskPriority;
            task.TaskType = dto.TaskType == 0 || dto.TaskType.Equals(null) ? task.TaskType : dto.TaskType;

            await taskRepository.UpdateAsync(task.ToProjectModel<MasterTask>());

            var ids = task.Assignees.Select(x => x.EmployeeId);
            var empList = new List<TaskUser>();
            foreach (var empId in ids)
            {
                var empSpec = new EmpSpecification(x => x.EmployeeId == empId);
                var taskUser = new TaskUser
                {
                    EmployeeId = empId,
                    Employee = await empRepository.GetSingleAsync(empSpec),
                    TaskId = id
                };
                empList.Add(taskUser);
            }

            task.Assignees = empList;

            return task.ToProjectModel<TaskDto>();
        }

        public async Task<TaskDto> UpdateAssignees(int id,TaskDto dto)
        {
            var spec = new TaskSpecification(x => x.TaskId == id);
            var task = await taskRepository.GetSingleWithNoTrackAsync(spec);
            var existingUser = task.Assignees.Select(x => x.EmployeeId).ToList();
            var newUsers = dto.Assignees.Select(x => x.EmployeeId).ToList();

            foreach (var remove in existingUser)
            {
                var exists = newUsers.Contains(remove);

                if (!exists)
                    await taskUserRepository.DeleteAsync(new TaskUser
                    {
                        EmployeeId = remove,
                        TaskId = id
                    });
            }

            foreach(var add in newUsers)
            {
                var exist = existingUser.Contains(add);

                if (!exist)
                    await taskUserRepository.AddAsync(new TaskUser
                    {
                        EmployeeId = add,
                        TaskId = id
                    });
            }

            var entity = await taskRepository.GetSingleWithNoTrackAsync(spec);

            var ids = entity.Assignees.Select(x => x.EmployeeId);
            var empList = new List<TaskUser>();
            foreach(var empId in ids)
            {
                var empSpec = new EmpSpecification(x => x.EmployeeId == empId);
                var taskUser = new TaskUser
                {
                    EmployeeId = empId,
                    Employee = await empRepository.GetSingleWithNoTrackAsync(empSpec),
                    TaskId = id
                };
                empList.Add(taskUser);
            }

            entity.Assignees = empList;

            return entity.ToProjectModel<TaskDto>();
        }

        private async Task<int> calculateListPosition(int columnId)
        {
            var tasks = await taskRepository.GetAsync(x => x.ColumnId == columnId);
            return tasks.Count > 0 ? tasks.Max(x => x.ListPosition) + 1 : 1;
        }

        public async Task SortingTaskOrder(SortTaskRequest sort)
        {
            var tasks = await taskRepository.GetAllAsync();

            var startIndex = 0;
            var changed = false;

            foreach(var order in sort.Order)
            {
                var task = tasks.Where(x => x.TaskId == order).FirstOrDefault();
                if(task.ListPosition != startIndex)
                {
                    changed = true;
                    task.ListPosition = startIndex;
                }

                var taskIds = sort.ProjectColumns.Where(x => x.Id == task.ColumnId).FirstOrDefault();
                var exist = taskIds.Ids.Contains(task.TaskId);

                if (!exist)
                {
                    foreach(var col in sort.ProjectColumns)
                    {
                        if (col.Ids.Contains(task.TaskId))
                        {
                            changed = true;
                            task.ColumnId = col.Id;
                            break;
                        }
                    }
                }

                if (changed == true)
                    await taskRepository.UpdateAsync(task);

                startIndex += 1;
                changed = false;
            }
        }

        public async Task DeleteCommentAsync(int id)
        {
            var delete = await taskCommentRepository.GetSingleAsync(x => x.CommentId == id);
            await taskCommentRepository.DeleteAsync(delete);
        }
    }
}
