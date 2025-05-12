using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using ProductCatalog.Data.DbContext;
    using ProductCatalog.Data.Interfaces;
    using System.Linq.Expressions;

    namespace ProductApi.Data
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            protected readonly ApplicationDbContext _context;
            private readonly DbSet<T> _entities;

            public Repository(ApplicationDbContext context)
            {
                _context = context;
                _entities = _context.Set<T>();
            }

            public async Task<T?> GetByIdAsync(int id) => await _entities.FindAsync(id);
            public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();
            public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
                await _entities.Where(predicate).ToListAsync();
            public async Task AddAsync(T entity) => await _entities.AddAsync(entity);
            public void Remove(T entity) => _entities.Remove(entity);
            public void Update(T entity) => _entities.Update(entity);
            public async Task<List<T>> FindAllIncludingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
            {
                IQueryable<T> query = _entities.Where(predicate);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return await query.ToListAsync();
            }

        }
    }

}
