using Microsoft.EntityFrameworkCore;
using WA_Pharmacy.EFCore.Config;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.EFCore.UnitOfWork;
using IdentityDbContext = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext;

namespace WA_Pharmacy.EFCore.DbContextFolder
{
    public class PharmacyContext : IdentityDbContext, IUnitOfWork
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Insured> Insureds { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<SalesHeader> SalesHeaders { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 3. نکته حیاتی: این خط باید حتما اول باشه تا تنظیمات Identity اعمال بشه
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            // نیازی نیست تک تک بنویسی، همین یک خط کل Configهای پروژه رو میخونه
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorConfig).Assembly);
        }

        public async Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }
}