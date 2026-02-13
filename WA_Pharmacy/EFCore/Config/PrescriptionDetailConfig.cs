using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class PrescriptionDetailConfig : IEntityTypeConfiguration<PrescriptionDetail>
    {
        public void Configure(EntityTypeBuilder<PrescriptionDetail> builder)
        {
            builder.HasKey(ml => ml.Id);

            // Money Columns
            builder.Property(ml => ml.OrginalPrice).HasColumnType("decimal(18,2)");
            builder.Property(ml => ml.InsurancePrice).HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasOne(ml => ml.Prescription)
                .WithMany(p => p.MedicineList) // Note: Fix spelling in your Entity class if needed
                .HasForeignKey(ml => ml.PrescriptionId);

            builder.HasOne(ml => ml.Medicine)
                .WithMany(m => m.PrescriptionItems)
                .HasForeignKey(ml => ml.MedicineId);
        }
    }
}
