using System.Linq.Expressions;

namespace Chat.Domain.Abstraction;

public interface IBaseRepository<T> where T : class
{
     Task<List<T>> GetAll();

     Task InsertAsync(T entity);

     Task<T?> GetSingleByConditionAsync(
          Expression<Func<T, bool>> expression, 
          params Expression<Func<T, object>>[] includes);
     
     Task<List<T>> GetByConditionsAsync( Expression<Func<T, bool>> expression, 
         params Expression<Func<T, object>>[] includes);
     
     Task Delete(Guid id);
     
     Task Delete(T entity);

     Task DeleteMany(Expression<Func<T, bool>> predicate);
     
     void Update(T entity);


}