using System.Linq.Expressions;

namespace WA_Pharmacy.AppCode.Repository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task<bool> SaveChangesAsync();

        Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetSomeAsync(int count);


        Task<List<TDto>> GetProjectedAsync<TDto>(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TDto>> GetProjectedAsync<TDto>(int count, Expression<Func<TEntity, bool>> predicate = null);

    }
}
