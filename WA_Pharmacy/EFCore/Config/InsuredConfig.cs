using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class InsuredConfig : IEntityTypeConfiguration<Insured>
    {
        public void Configure(EntityTypeBuilder<Insured> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.InsuranceNumber)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength(); // Added this to make it 'Char(8)'

            // Fix: Configure dates separately
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.ExpireDate).IsRequired();

            // Relationships (Excellent work here!)
            builder.HasOne(p => p.Customer)
                .WithMany(p => p.InsuredRecords)
                .HasForeignKey(p => p.CustomerId);

            builder.HasOne(p => p.Insurance)
                .WithMany(p => p.InsuredList)
                .HasForeignKey(p => p.InsuranceId);
        }
    }
}
