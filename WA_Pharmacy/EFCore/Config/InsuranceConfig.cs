using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class InsuranceConfig : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.CompanyName).IsRequired().HasMaxLength(50);
        }
    }
}
