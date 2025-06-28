using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        //public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        //{
        //    var query = context.Set<T>().AsQueryable();
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }
        //    return await query.AsNoTracking().ToListAsync();
        //}        
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> queryShaper)
        {
            var query = context.Set<T>().AsNoTracking();
            if(queryShaper != null)
            {
                query = queryShaper(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        //public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        //{
        //    var query = context.Set<T>().AsQueryable();
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }
        //    return await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        //}        
        public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> queryShaper)
        {
            var query = context.Set<T>().AsNoTracking();
            if (queryShaper != null)
            {
                query = queryShaper(query);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }

    }
}
