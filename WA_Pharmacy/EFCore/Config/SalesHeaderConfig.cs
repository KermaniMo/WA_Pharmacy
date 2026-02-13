using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class SalesHeaderConfig : IEntityTypeConfiguration<SalesHeader>
    {
        public void Configure(EntityTypeBuilder<SalesHeader> builder)
        {
            builder.HasKey(sh => sh.Id);
            builder.Property(sh => sh.SaleDate).IsRequired();
            builder.Property(x => x.SaleNumber).IsRequired().HasMaxLength(8).IsFixedLength();

            // Optional: If you added TotalPrice later
            // builder.Property(sh => sh.TotalPrice).HasColumnType("decimal(18,2)");

            // Relationships
            // Prescription is Optional (Nullable in DB)
            builder.HasOne(sh => sh.Prescription)
                .WithMany() // Assuming Prescription doesn't list all its SalesHeaders
                .HasForeignKey(sh => sh.PrescriptionId)
                .IsRequired(false); // Explicitly stating it is nullable
        }
    }
}
