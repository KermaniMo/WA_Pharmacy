using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Prescription : BaseEntity<long>
    {

        public string TrackingCode { get; set; } // Char(8)
        public decimal TotalPrice { get; set; } // قیمت کل نسخه

        // Foreign Keys
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public long? InsuredId { get; set; }
        public Insured Insured { get; set; }

        // The list of medicines in this prescription
        public ICollection<PrescriptionDetail> MedicineList { get; set; }

    }
}
