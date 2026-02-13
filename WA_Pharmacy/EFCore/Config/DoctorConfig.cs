
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DoctorNumber).IsRequired().HasMaxLength(8).IsFixedLength();
            builder.Property(x => x.NationalCode).IsRequired().HasMaxLength(10).IsFixedLength();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            // Index for fast searching
            builder.HasIndex(x => x.DoctorNumber).IsUnique();


            // تنظیم رابطه یک-به-یک
            builder.HasOne(d => d.User)           // هر دکتر یک یوزر دارد
                .WithMany()                    // (نکته طلایی اینجاست!)
                .HasForeignKey(d => d.UserId)  // کلید خارجی اینجاست
                .IsRequired();                 // هر دکتری حتماً باید یوزر داشته باشه

        }
    }
}
