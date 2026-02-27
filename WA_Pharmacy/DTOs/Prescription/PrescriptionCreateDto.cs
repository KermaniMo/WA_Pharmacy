namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionCreateDto
    {
        public long CustomerId { get; set; }
        public int DoctorId { get; set; }
        public long? InsuredId { get; set; }

        public List<PrescriptionItemDto> Items { get; set; } = new List<PrescriptionItemDto>();
    }
}
