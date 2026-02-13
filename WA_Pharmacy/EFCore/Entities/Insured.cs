using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Insured : BaseEntity<long>
    {

        public string InsuranceNumber { get; set; } // Char(8)
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Discount { get; set; }

        // Foreign Keys
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        public long InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}
