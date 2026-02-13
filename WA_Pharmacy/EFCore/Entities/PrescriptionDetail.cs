using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class PrescriptionDetail : BaseEntity<long>
    {

        public decimal OrginalPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public byte Quantity { get; set; } // TinyInt

        // Foreign Keys
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public long PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

    }
}
