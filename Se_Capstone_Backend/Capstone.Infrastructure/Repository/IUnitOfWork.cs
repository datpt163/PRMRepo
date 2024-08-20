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
        public IRepository<New> News { get; }
        public IRepository<Permission> Permissions { get; }
        public IRepository<Project> Projects { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Sprint> Sprints { get; }
        public IRepository<Staff> Staffs { get; }
        public IRepository<Status> Statuses { get; }
        public IRepository<User> Users { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync();

        Task Dispose(bool disposing);
    }
}
