using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WA_Pharmacy.EFCore.UnitOfWork;

namespace WA_Pharmacy.AppCode.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(TKey id, params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<TKey>(e, "Id").Equals(id));
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetSomeAsync(int count)
        {
            return await _dbSet.Take(count).ToListAsync();
        }

        public async Task<List<TDto>> GetProjectedAsync<TDto>(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<TDto>> GetProjectedAsync<TDto>(int count, Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate)
                    .Take(count);
            }

            return await query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .Take(count)
                .ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
