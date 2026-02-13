using WA_Pharmacy.AppCode;

namespace WA_Pharmacy.EFCore.Entities
{
    public class SalesDetails : BaseEntity<long>
    {


        public byte Quantity { get; set; }
        public decimal Price { get; set; }

        // Foreign Keys
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public long SalesHeaderId { get; set; }
        public SalesHeader SalesHeader { get; set; }

    }
}
