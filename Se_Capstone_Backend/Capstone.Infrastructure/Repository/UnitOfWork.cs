using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SeCapstoneContext _context;
        private bool disposed = false;
        public UnitOfWork(SeCapstoneContext context)
        {
            _context = context;
        }
        public IRepository<User> users = null!;
        public IRepository<User> Users => users ?? new Repository<User>(_context);

        public int SaveChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        //public Task BulkSaveChangesAsync()
        //{
        //    return _context.BulkSaveChangesAsync();
        //}

        public IRepository<T> AsyncRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public async Task Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            disposed = true;
        }
    }
}
