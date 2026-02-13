using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class SalesHeader : BaseEntity<long>
    {



        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }

        // Only linked to Prescription (Nullable)
        public long? PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        // List of items in this sale
        public ICollection<SalesDetails> SalesDetails { get; set; }

    }
}
