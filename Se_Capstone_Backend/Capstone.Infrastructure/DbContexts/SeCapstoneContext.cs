using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Infrastructure.DbContexts
{
    public partial class SeCapstoneContext : IdentityDbContext<User, Role, Guid>
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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LeaveLog> LeaveLogs { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Skills)
            .WithMany(s => s.Users);

            modelBuilder.Entity<Applicant>()
                .Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Permission>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Attendance>()
               .Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Comment>()
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

            modelBuilder.Entity<Position>()
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


            modelBuilder.Entity<Status>()
              .Property(e => e.Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<GroupPermission>()
             .Property(e => e.Id)
             .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<User>()
             .Property(e => e.Gender)
             .HasColumnType("smallint")
              .HasConversion<int>()
            .HasDefaultValue(Gender.Male);

            modelBuilder.Entity<User>()
            .Property(e => e.Status)
            .HasColumnType("smallint")
            .HasConversion<int>()
            .HasDefaultValue(UserStatus.Active);

            modelBuilder.Entity<Issue>()
           .Property(e => e.Priority)
           .HasColumnType("smallint")
            .HasConversion<int>()
            .HasDefaultValue(Priority.Low);

            modelBuilder.Entity<Project>()
          .Property(e => e.Status)
          .HasColumnType("smallint")
           .HasConversion<int>()
           .HasDefaultValue(ProjectStatus.InProgress);

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.User)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<LeaveLog>()
             .HasOne(l => l.User)
             .WithMany(s => s.LeaveLogs)
             .HasForeignKey(l => l.UserId);


            modelBuilder.Entity<Issue>()
              .HasOne(a => a.Label)
               .WithMany(s => s.Issues)
               .HasForeignKey(a => a.LabelId);

            modelBuilder.Entity<Comment>()
            .HasOne(a => a.User)
             .WithMany(s => s.Comments)
             .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Label>()
          .HasOne(a => a.Project)
           .WithMany(s => s.Labels)
           .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Status>()
        .HasOne(a => a.Project)
         .WithMany(s => s.Statuses)
         .HasForeignKey(a => a.ProjectId);


            modelBuilder.Entity<Comment>()
            .HasOne(a => a.Issue)
             .WithMany(s => s.Comments)
             .HasForeignKey(a => a.IssueId);

            modelBuilder.Entity<Issue>()
            .HasOne(a => a.ParentIssue)
            .WithMany(s => s.SubIssues)
            .HasForeignKey(a => a.ParentIssueId);

            modelBuilder.Entity<Issue>()
           .HasOne(a => a.LastUpdateBy)
           .WithMany(s => s.IssuesUpdate)
           .HasForeignKey(a => a.LastUpdateById);



            modelBuilder.Entity<Issue>()
             .HasOne(a => a.Status)
              .WithMany(s => s.Issues)
              .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<Issue>()
             .HasOne(a => a.Project)
              .WithMany(s => s.Issues)
              .HasForeignKey(a => a.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                  .HasOne(a => a.Lead)
                   .WithMany(s => s.LeadProjects)
                   .HasForeignKey(a => a.LeadId);

            modelBuilder.Entity<Permission>()
              .HasOne(a => a.GroupPermission)
               .WithMany(s => s.Permissions)
               .HasForeignKey(a => a.GroupPermissionId);

            modelBuilder.Entity<Applicant>()
           .HasOne(a => a.MainJob)
           .WithMany()
           .HasForeignKey(a => a.MainJobId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
          .HasOne(a => a.Position)
          .WithMany(x => x.Users)
          .HasForeignKey(a => a.PositionId)
          .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Applicant>()
              .HasMany(a => a.Jobs)
              .WithMany(j => j.Applicants)
              .UsingEntity<Dictionary<string, object>>(
                  "applicantJobs",
                  j => j.HasOne<Job>().WithMany().HasForeignKey("JobId"),
                  a => a.HasOne<Applicant>().WithMany().HasForeignKey("ApplicantId"));

            modelBuilder.Entity<Project>()
             .HasMany(a => a.Users)
             .WithMany(j => j.Projects)
             .UsingEntity<Dictionary<string, object>>(
                 "projectUsers",
                 j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                 a => a.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

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
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp without time zone");
                    }
                }
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
