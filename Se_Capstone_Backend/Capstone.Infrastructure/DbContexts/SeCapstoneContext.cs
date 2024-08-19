using Capstone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<New> News { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Applicant>()
              .HasMany(a => a.Jobs)
              .WithMany(j => j.Applicants)
              .UsingEntity<Dictionary<string, object>>(
                  "ApplicantJob",
                  j => j.HasOne<Job>().WithMany().HasForeignKey("JobId"),
                  a => a.HasOne<Applicant>().WithMany().HasForeignKey("ApplicantId"));

            modelBuilder.Entity<Project>()
             .HasMany(a => a.Staffs)
             .WithMany(j => j.Projects)
             .UsingEntity<Dictionary<string, object>>(
                 "ApplicantJob",
                 j => j.HasOne<Staff>().WithMany().HasForeignKey("StaffId"),
                 a => a.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            modelBuilder.Entity<Project>()
            .HasMany(a => a.Sprints)
            .WithMany(j => j.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ApplicantJob",
                j => j.HasOne<Sprint>().WithMany().HasForeignKey("SprintId"),
                a => a.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            modelBuilder.Entity<Sprint>()
           .HasMany(a => a.Issues)
           .WithMany(j => j.Sprints)
           .UsingEntity<Dictionary<string, object>>(
            "ApplicantJob",
               j => j.HasOne<Capstone.Domain.Entities.Issue>().WithMany().HasForeignKey("IssueId"),
               a => a.HasOne<Sprint>().WithMany().HasForeignKey("SprintId"));

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<LeaveLog>()
             .HasOne(l => l.Staff)
             .WithMany(s => s.LeaveLogs)
             .HasForeignKey(l => l.StaffId);

        
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
