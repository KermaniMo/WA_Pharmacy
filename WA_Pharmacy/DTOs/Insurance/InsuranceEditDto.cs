using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Insurance
{
    public class InsuranceEditDto
    {
        public long Id { get; set; }

        [Display(Name = "نام شرکت بیمه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string CompanyName { get; set; }
    }
}
