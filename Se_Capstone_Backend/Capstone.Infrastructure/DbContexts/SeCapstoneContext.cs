using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.DbContexts
{
    public partial class SeCapstoneContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SeCapstoneContext()
        {
        }

        public SeCapstoneContext(DbContextOptions<SeCapstoneContext> options)
    : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=se-capstone-fpt-5427.k.aivencloud.com:21222;Database=SeCapstone;Username=avnadmin;Password=AVNS_ZPTphEdG3DfvRXb-xUQ");
            }
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LeaveLog> LeaveLogs { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Permission>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Attendance>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Issue>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Job>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Label>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<LeaveLog>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<LogEntry>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Article>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Project>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Sprint>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Staff>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Status>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<User>()
             .Property(e => e.Gender)
             .HasColumnType("smallint");

            modelBuilder.Entity<User>()
            .Property(e => e.Status)
            .HasColumnType("smallint");

            modelBuilder.Entity<Issue>()
           .Property(e => e.Priority)
           .HasColumnType("smallint");

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Staff)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<LeaveLog>()
             .HasOne(l => l.Staff)
             .WithMany(s => s.LeaveLogs)
             .HasForeignKey(l => l.StaffId);

            modelBuilder.Entity<Issue>()
            .HasOne(l => l.Staff)
            .WithMany(s => s.Issues)
            .HasForeignKey(l => l.AssignedId);

            modelBuilder.Entity<Issue>()
              .HasOne(a => a.Label)
               .WithMany(s => s.Issues)
               .HasForeignKey(a => a.LabelId);

            modelBuilder.Entity<Issue>()
             .HasOne(a => a.Status)
              .WithMany(s => s.Issues)
              .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<Issue>()
             .HasOne(a => a.Project)
              .WithMany(s => s.Issues)
              .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Applicant>()
               .HasOne(a => a.Staff)
                .WithMany(s => s.Applicants)
                .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Staff)
            .WithOne(s => s.User) 
            .HasForeignKey<Staff>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Applicant>()
              .HasMany(a => a.Jobs)
              .WithMany(j => j.Applicants)
              .UsingEntity<Dictionary<string, object>>(
                  "applicantJobs",
                  j => j.HasOne<Job>().WithMany().HasForeignKey("JobId"),
                  a => a.HasOne<Applicant>().WithMany().HasForeignKey("ApplicantId"));

            modelBuilder.Entity<Project>()
             .HasMany(a => a.Staffs)
             .WithMany(j => j.Projects)
             .UsingEntity<Dictionary<string, object>>(
                 "projectStaffs",
                 j => j.HasOne<Staff>().WithMany().HasForeignKey("StaffId"),
                 a => a.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            modelBuilder.Entity<Project>()
            .HasMany(a => a.Sprints)
            .WithMany(j => j.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "projectSprints",
                j => j.HasOne<Sprint>().WithMany().HasForeignKey("SprintId"),
                a => a.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            modelBuilder.Entity<Sprint>()
           .HasMany(a => a.Issues)
           .WithMany(j => j.Sprints)
           .UsingEntity<Dictionary<string, object>>(
            "sprintIssues",
               j => j.HasOne<Issue>().WithMany().HasForeignKey("IssueId"),
               a => a.HasOne<Sprint>().WithMany().HasForeignKey("SprintId"));

            modelBuilder.Entity<Permission>()
          .HasMany(a => a.Roles)
          .WithMany(j => j.Permissions)
          .UsingEntity<Dictionary<string, object>>(
           "rolePermissions",
              j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
              a => a.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    string tableNameSub = tableName.Substring(6);
                    if (tableNameSub.Length > 0)
                    {
                        tableNameSub = char.ToLower(tableNameSub[0]) + tableNameSub.Substring(1);
                    }
                    entityType.SetTableName(tableNameSub);
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var columnName = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                    property.SetColumnName(columnName);
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetColumnType("timestamp without time zone");
                    }
                }
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
