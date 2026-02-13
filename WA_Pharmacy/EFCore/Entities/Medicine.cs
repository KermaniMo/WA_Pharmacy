using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class Medicine : BaseEntity<int>
    {


        public string MedicineName { get; set; }
        public decimal Price { get; set; }
        public bool IsInsurance { get; set; }
        public int Stock { get; set; }
        public ICollection<PrescriptionDetail> PrescriptionItems { get; set; }
        public ICollection<SalesDetails> SalesItems { get; set; }

    }
}
