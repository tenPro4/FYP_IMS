using EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Context
{
    public class AppDbContext : IdentityDbContext<MasterAccount,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeImage> EmployeeImage { get; set; }
        public virtual DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public virtual DbSet<MasterAccount> MasterAccount { get; set; }
        public virtual DbSet<MasterDepartment> MasterDepartment { get; set; }
        public virtual DbSet<MasterPermission> MasterPermission { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EmployeePermission> EmployeePermission { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Leave> Leave { get; set; }
        public virtual DbSet<SupportFile> SupportFile { get; set; }

        //project
        public virtual DbSet<MasterProject> MasterProject { get; set; }
        public virtual DbSet<ProjectColumn> ProjectColumn { get; set; }
        public virtual DbSet<MasterTask> MasterTask { get; set; }
        public virtual DbSet<TaskUser> TaskUser { get; set; }
        public virtual DbSet<ProjectUser> ProjectUser { get; set; }
        public virtual DbSet<TaskComment> TaskComment { get; set; }

        //refreshtoken
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region employeeInfo

            builder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.EmployeeId);

                entity.Property(e => e.CardNo)
                   .IsRequired()
                   .IsUnicode(false);

                entity.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Gender)
                   .IsRequired()
                   .HasColumnType("nchar(1)")
                   .HasDefaultValue("('m')");

                entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())"); ;

                entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

                //entity.Property(e => e.Status).HasDefaultValueSql("('1')");

                entity.Property(e => e.ChangedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

                //entity.HasOne(x => x.MasterAccount)
                //.WithOne(e => e.Employee)
                //.HasForeignKey<MasterAccount>(k => k.Id);
            });
            builder.Entity<EmployeeImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("EmployeeID")
                    .IsUnicode(false);
            });
            builder.Entity<EmployeeAddress>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeAddressId });

                entity.Property(e => e.EmployeeAddressId).HasColumnName("EmployeeAddressID");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("EmployeeID")
                    .IsUnicode(false);
            });
            builder.Entity<MasterAccount>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
            //department
            builder.Entity<MasterDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            builder.Entity<MasterPermission>(entity =>
            {
                entity.HasKey(x => x.PermissionId);

                entity.Property(x => x.PermissionId).HasColumnName("PermissionID");
            });
            builder.Entity<EmployeePermission>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeId,x.PermissionId });
            });
            builder.Entity<EmployeeDepartment>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeId, x.DepartmentId });
            });
            builder.Entity<Log>(log =>
            {
                log.HasKey(x => x.Id);
                log.Property(x => x.Level).HasMaxLength(128);
            });

            builder.Entity<Attendance>(entity =>
            {
                entity.HasIndex(e => new { e.EmployeeId, e.WorkDate })
                   .HasName("idx1_AttendanceC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TimeIn)
                   .IsRequired()
                   .HasColumnName("Time_In")
                   .HasMaxLength(19)
                   .IsUnicode(false);

                entity.Property(e => e.TimeOut)
                    .HasColumnName("Time_Out")
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.WorkDate)
                    .IsRequired()
                    .HasColumnName("Work_Date")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorkDay)
                    .HasColumnName("Work_Day")
                    .HasColumnType("decimal(2, 1)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkShift)
                    .IsRequired()
                    .HasColumnName("Work_Shift")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });
            builder.Entity<ProjectUser>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeId, x.ProjectId });
            });
            builder.Entity<TaskUser>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeId, x.TaskId });
            });

            #endregion employeeInfo

        }
    }
}
