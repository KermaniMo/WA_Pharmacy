using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Prescription
{
    public class PrescriptionDto
    {
        [Display(Name = "شناسه نسخه")]
        public long Id { get; set; }

        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        [Display(Name = "مبلغ کل (تومان)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TotalPrice { get; set; }

        public long CustomerId { get; set; }
        [Display(Name = "نام بیمار")]
        public string CustomerFullName { get; set; }

        public int DoctorId { get; set; }
        [Display(Name = "نام پزشک")]
        public string DoctorName { get; set; }

        public long? InsuredId { get; set; }
        [Display(Name = "شماره بیمه")]
        public string InsuranceNumber { get; set; }

        [Display(Name = "اقلام دارو")]
        public List<PrescriptionDetailDto> MedicineList { get; set; } = new List<PrescriptionDetailDto>();
    }
}
