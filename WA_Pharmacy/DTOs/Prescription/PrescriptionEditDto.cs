namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionEditDto
    {
        public long Id { get; set; }
        public string? TrackingCode { get; set; }

        // فقط برای نمایش (غیرقابل ویرایش)
        public long CustomerId { get; set; }
        public string? CustomerFullName { get; set; }

        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }

        public long? InsuredId { get; set; }
        public string? InsuranceNumber { get; set; }

        // قابل ویرایش
        public List<PrescriptionItemDto> Items { get; set; } = new List<PrescriptionItemDto>();
    }
}
