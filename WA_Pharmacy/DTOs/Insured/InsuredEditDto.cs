using System.ComponentModel.DataAnnotations;
using WA_Pharmacy.DTOs.Customer;
using WA_Pharmacy.DTOs.Insurance;

namespace WA_Pharmacy.DTOs.Insured
{
    public class InsuredEditDto
    {
        public long Id { get; set; }

        [Display(Name = "شماره بیمه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "{0} باید دقیقا {1} کاراکتر باشد")]
        public string InsuranceNumber { get; set; }

        [Display(Name = "تاریخ شروع اعتبار")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان اعتبار")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string ExpireDate { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "{0} باید بین {1} و {2} باشد")]
        public decimal Discount { get; set; }

        [Display(Name = "مشتری")]
        [Required(ErrorMessage = "لطفاً {0} را انتخاب کنید")]
        public long CustomerId { get; set; }

        [Display(Name = "شرکت بیمه")]
        [Required(ErrorMessage = "لطفاً {0} را انتخاب کنید")]
        public long InsuranceId { get; set; }
    }
}
