using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Medicine
{
    public class MedicineEditDto
    {
        public int Id { get; set; }

        [Display(Name = "نام دارو")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string MedicineName { get; set; }

        [Display(Name = "قیمت فروش")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت نمی‌تواند منفی باشد")]
        public decimal Price { get; set; }

        [Display(Name = "موجودی اولیه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [Range(0, int.MaxValue, ErrorMessage = "موجودی نمی‌تواند منفی باشد")]
        public int Stock { get; set; }

        [Display(Name = "شامل بیمه است؟")]
        public bool IsInsurance { get; set; }


    }
}
