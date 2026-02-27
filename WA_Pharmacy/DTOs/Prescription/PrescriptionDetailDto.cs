namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionDetailDto
    {
        public long Id { get; set; }
        public decimal OrginalPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public byte Quantity { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }
}
