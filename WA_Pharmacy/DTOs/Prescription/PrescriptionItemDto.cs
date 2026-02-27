namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionItemDto
    {
        public int MedicineId { get; set; }
        public byte Quantity { get; set; }


        public decimal InsurancePrice { get; set; }
    }
}
