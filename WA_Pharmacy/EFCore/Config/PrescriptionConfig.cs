using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {

        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TrackingCode).IsRequired().HasMaxLength(8).IsFixedLength();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)"); // قیمت کل نسخه

            // Relationships
            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Prescriptions)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting customer if prescription exists

            builder.HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(p => p.Insured)
                .WithMany()
                .HasForeignKey(p => p.InsuredId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
