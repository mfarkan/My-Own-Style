using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Shared
{
    public abstract class GenericRepository<TContext> : IGenericRepository where TContext : DbContext
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : EntityBase
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange<T>(params T[] entities) where T : EntityBase
        {
            _context.Set<T>().AddRange(entities);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Delete<T>(T entity) where T : EntityBase
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<T>> GetAllAsync<T>() where T : EntityBase
        {
            return await _context.Set<T>().Where(q => q.Status == Core.Enumarations.StatusType.Active).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid Id) where T : EntityBase
        {
            var query = _context.Set<T>().Where(q => q.Id == Id && q.Status == Core.Enumarations.StatusType.Active);
            return await query.FirstOrDefaultAsync();
        }
        public IQueryable<T> QueryWithoutTracking<T>() where T : EntityBase
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            return query;
        }
        public IQueryable<T> Query<T>() where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();
            return query;
        }

        public void Update<T>(T entity) where T : EntityBase
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
