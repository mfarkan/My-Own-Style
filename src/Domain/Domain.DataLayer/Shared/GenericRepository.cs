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
        public void Add<T>(params T[] entities) where T : EntityBase
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

        public IQueryable<T> Query<T>() where T : EntityBase
        {
            var query = _context.Set<T>().AsQueryable();
            return query;
        }

        public void Update<T>(T entity) where T : EntityBase
        {
            _context.Set<T>().Update(entity);
        }
    }
}
