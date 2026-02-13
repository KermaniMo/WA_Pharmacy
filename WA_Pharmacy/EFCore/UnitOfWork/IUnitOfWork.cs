using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace WA_Pharmacy.EFCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        DbSet<Tentity> Set<Tentity>() where Tentity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// شروع تراکنش برای عملیات اتمیک
        /// </summary>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
