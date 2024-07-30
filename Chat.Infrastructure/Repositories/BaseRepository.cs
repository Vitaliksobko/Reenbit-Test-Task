using System.Linq.Expressions;
using Chat.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;



namespace Chat.infrastructure.Repositories;

public class BaseRepository<T>(ChatDbContext context) : IBaseRepository<T> where T : class
{
   
    
        public async Task<List<T>> GetAll()
        {
            var query =  context.Set<T>().AsNoTracking();

            return await query.ToListAsync();
        }
        
        
        public async Task InsertAsync(T entity)
        {
            var query = context.Set<T>().AddAsync(entity);

            await query;
            await context.SaveChangesAsync();
        }
        public async Task DeleteMany(Expression<Func<T, bool>> predicate)
        {
            var items = context.Set<T>().Where(predicate);
            context.RemoveRange(items);
            await context.SaveChangesAsync();
        }
        public async Task<T?> GetSingleByConditionAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>()
                .Where(expression)
                .AsNoTracking();

            return await includes
                .Aggregate(result, (current, next) => current.Include(next))
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<T>> GetByConditionsAsync(
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            var query = context.Set<T>()
                .Where(expression)
                .AsNoTracking();

            return await includes
                .Aggregate(query, (current, next) => current.Include(next))
                .ToListAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var t = await context.Set<T>().FindAsync(id);

            context.Remove(t);
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        
       
}