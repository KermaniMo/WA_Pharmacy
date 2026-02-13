using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WA_Pharmacy.EFCore.Entities;

namespace WA_Pharmacy.EFCore.Config
{
    public class SalesDetailsConfig : IEntityTypeConfiguration<SalesDetails>
    {
        public void Configure(EntityTypeBuilder<SalesDetails> builder)
        {
            // Relationship: One Header has Many Details
            builder.HasOne(x => x.SalesHeader)
                .WithMany(x => x.SalesDetails)
                .HasForeignKey(x => x.SalesHeaderId);

            // Configuring Decimal (Money)
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
