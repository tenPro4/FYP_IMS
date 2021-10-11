using EntityFramework.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationSetting.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAccount> EmployeeAccount { get; set; }
        public virtual DbSet<EmployeeImage> EmployeeImage { get; set; }
        public virtual DbSet<MasterAccount> MasterAccount { get; set; }
        public virtual DbSet<MasterAccountAuthority> MasterAccountAuthority { get; set; }
        public virtual DbSet<MasterAuthority> MasterAuthority { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Log>(log =>
            {
                log.HasKey(x => x.Id);
                log.Property(x => x.Level).HasMaxLength(128);
            });

            builder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.GlobalId)
                   .HasName("UQ__Employee__A50E8993D0F46EBD")
                   .IsUnique();

                entity.Property(e => e.GlobalId)
                    .IsRequired()
                    .HasColumnName("GlobalID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ChangedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeType)
                    .IsRequired()
                    .HasColumnType("nchar(2)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("nchar(1)");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.HireType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            builder.Entity<MasterAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__MasterAc__C9F28456EC943ACC")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(50);
            });

            builder.Entity<MasterAccountAuthority>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.AuthorityId });

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AuthorityId).HasColumnName("AuthorityID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.MasterAccountAuthority)
                    .HasForeignKey(d => d.AccountId);

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.MasterAccountAuthority)
                    .HasForeignKey(d => d.AuthorityId)
                    .HasConstraintName("FK_MasterUserAuthority_MasterAuthority_AuthorityID");
            });

            builder.Entity<MasterAuthority>(entity =>
            {
                entity.HasKey(e => e.AuthorityId);

                entity.Property(e => e.AuthorityId).HasColumnName("AuthorityID");

                entity.Property(e => e.AuthorityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            builder.Entity<EmployeeAccount>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.AccountId });

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.EmployeeAccount)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_EmployeeUser_MasterUser_UserID");

                entity.HasOne(d => d.Employee)
                   .WithMany(p => p.EmployeeAccount)
                   .HasForeignKey(d => d.EmployeeId)
                   .HasConstraintName("FK_EmployeeUser_Employee_EmployeeID");

            });

            builder.Entity<EmployeeImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("EmployeeID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeImage)
                    .HasForeignKey(d => d.EmployeeId);
            });
        }

    }
}
