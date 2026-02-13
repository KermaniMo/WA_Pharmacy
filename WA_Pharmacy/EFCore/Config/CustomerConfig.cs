using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NationalCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength(); // Char(10)

            builder.Property(c => c.MobileNumber)
                .HasMaxLength(11)
                .IsFixedLength(); // Char(11) - Nullable in DB

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.RegisterDate).IsRequired();

            // Index for faster search
            builder.HasIndex(c => c.NationalCode);
        }

    }
}
