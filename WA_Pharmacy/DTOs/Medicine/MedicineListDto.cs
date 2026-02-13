using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Medicine
{
    public class MedicineListDto
    {
        public int Id { get; set; }

        [Display(Name = "نام دارو")]
        public string MedicineName { get; set; }

        [Display(Name = "قیمت (ریال)")]
        public decimal Price { get; set; }
        [Display(Name = "موجودی")]
        public int Stock { get; set; }

        [Display(Name = "وضعیت بیمه")]
        public string IsInsurance { get; set; }

    }
}
