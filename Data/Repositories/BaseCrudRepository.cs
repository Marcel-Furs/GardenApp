using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace GardenApp.API.Data.Repositories
{
    public class BaseCrudRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        protected readonly DataContext context;
        protected readonly DbSet<T> table;

        public BaseCrudRepository(DataContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Add(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(TKey id)
        {
            var entity = await Get(id);
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return table.FirstOrDefault(predicate);
        }

        public async Task<T> Get(TKey id)
        {
            return await table.FindAsync(id);
        }
        public async Task<List<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> Get(Expression<Func<T, bool>> exp)
        {
            return await table.Where(exp).FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> exp)
        {
            return await table.Where(exp).ToListAsync();
        }
    }
}