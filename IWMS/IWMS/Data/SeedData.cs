using EntityFramework.Context;
using EntityFramework.Entities;
using IWMS.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IWMS.Configurations.Authorization.Permissions;

namespace IWMS.Data
{
    public static class SeedData
    {
        public static void InitializeData(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();

            #region task class
            //if (!context.TaskStatus.Any())
            //{
            //    var status = new List<EntityFramework.Entities.TaskStatus>()
            //    {
            //        new EntityFramework.Entities.TaskStatus
            //        {
            //            Name = "In open",
            //            Description = "In opening",
            //            StatusCode  = "s1",
            //            SortOrder = 1
            //        },
            //        new EntityFramework.Entities.TaskStatus
            //        {
            //            Name = "In Processing",
            //            Description = "In Processing",
            //            StatusCode  = "s2",
            //            SortOrder = 2
            //        },
            //        new EntityFramework.Entities.TaskStatus
            //        {
            //            Name = "In completing",
            //            Description = "In completing",
            //            StatusCode  = "s3",
            //            SortOrder = 3
            //        }
            //    };

            //    context.TaskStatus.AddRange(status);
            //    context.SaveChanges();
            //}

            //if (!context.TaskPriority.Any())
            //{
            //    var priorities = new List<TaskPriority>()
            //    {
            //        new TaskPriority
            //        {
            //            Name = "normal",
            //            Description = "In opening",
            //            PriorityCode  = "p1",
            //            SortOrder = 1
            //        },
            //        new TaskPriority
            //        {
            //            Name = "Important",
            //            Description = "In Processing",
            //            PriorityCode  = "p2",
            //            SortOrder = 2
            //        },
            //        new TaskPriority
            //        {
            //            Name = "Urgent",
            //            Description = "Urgent",
            //            PriorityCode  = "p3",
            //            SortOrder = 3
            //        }
            //    };
            //    context.TaskPriority.AddRange(priorities);
            //    context.SaveChanges();
            //}

            #endregion

            if (!context.MasterPermission.Any())
            {
                #region mock data
                //var permissions = new List<MasterPermission>()
                //{
                //    new MasterPermission
                //    {
                //        PermissionCode = "Create",
                //        PermissionName = "Create"
                //    },
                //    new MasterPermission
                //    {
                //        PermissionCode = "Delete",
                //        PermissionName = "Delete"
                //    },
                //    new MasterPermission
                //    {
                //        PermissionCode = "Update",
                //        PermissionName = "Update"
                //    },
                //    new MasterPermission
                //    {
                //        PermissionCode = "Get",
                //        PermissionName = "Get"
                //    },
                //};

                //context.MasterPermission.AddRange(permissions);
                #endregion

                var departments = EnumHelper.ToSelectList<DepartmentType>();
                var permissions = EnumHelper.ToSelectList<PermissionType>();

                foreach(var dep in departments)
                {
                    permissions.ForEach(x =>
                    {
                        var permission = new MasterPermission
                        {
                            PermissionCode = dep + "." + x.Text,
                            PermissionName = dep + "." + x.Text
                        };
                        context.MasterPermission.Add(permission);
                    });
                }

                context.SaveChanges();
            }

            if (!context.MasterDepartment.Any())
            {
                var departments = EnumHelper.ToSelectList<DepartmentType>();

                departments.ForEach(x =>
                {
                    var dep = new MasterDepartment
                    {
                        DepartmentCode = x.Text,
                        DepartmentName = x.Text
                    };
                    context.MasterDepartment.Add(dep);
                });

                //var deps = new List<MasterDepartment>
                //{
                //    new MasterDepartment
                //    {
                //        DepartmentCode = "UNCATEGORY",
                //        DepartmentName = "UNCATEGORY",
                //    }
                //};

                //context.MasterDepartment.AddRange(deps);
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                var roles = EnumHelper.ToSelectList<Roles>();

                roles.ForEach(x =>
                {
                    context.Roles.Add(new IdentityRole<int> {
                        Name = x.Text,
                        NormalizedName = x.Text.ToUpper()
                    });
                });

                context.SaveChanges();
            }

        }
    }
}
