using Capstone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        public IRepository<Applicant> Applicants { get; }
        public IRepository<Attendance> Attendances { get; }
        public IRepository<Issue> Issues { get; }
        public IRepository<Job> Jobs { get; }
        public IRepository<Label> Labels { get; }
        public IRepository<LeaveLog> LeaveLogs { get; }
        public IRepository<LogEntry> LogEntrys { get; }
        public IRepository<Article> Articles { get; }
        public IRepository<Permission> Permissions { get; }
        public IRepository<Project> Projects { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Status> Statuses { get; }
        public IRepository<User> Users { get; }
        public IRepository<Position> Positions { get; }
        public IRepository<Comment> Comments { get; }
        public IRepository<GroupPermission> GroupPermissions { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync();

        Task Dispose(bool disposing);
    }
}
