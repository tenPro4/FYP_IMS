using BusinessLogic.Helpers;
using BusinessLogic.Resources;
using BusinessLogic.Resources.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Entities.Base;
using EntityFramework.Repositories;
using EntityFramework.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
{
    public static class ServicesRegisterExtensions
    {
        public static IServiceCollection AddServices<TDbContext>(this IServiceCollection services)
          where TDbContext : DbContext
        {
            //model
            services.AddSingleton<IBaseEntity, BaseEntity>();

            //Repositories
            //services.AddTransient<IIdentityRepository, IdentityRepository<TDbContext>>();

            services.AddTransient<ILogRepository,LogRepository<TDbContext>>();
            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IGenericRepositor<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));

            //Services
            services.AddTransient<ILogService, LogService>();
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeImageService, EmployeeImageService>();
            services.AddTransient<IEmployeeAddressService, EmployeeAddressService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<IFileWriter, FileWriter>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectColumnService, ProjectColumnService>();
            services.AddTransient<ILeaveService, LeaveService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<UserManager<MasterAccount>>();
            services.AddTransient<IEventService, EventService>();

            //Resouces
            services.AddScoped<IAuthenticationServiceResources, AuthenticationServiceResources>();

            return services;
        }
    }
}
