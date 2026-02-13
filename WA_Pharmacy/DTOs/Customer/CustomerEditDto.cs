using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Customer
{
    public class CustomerEditDto
    {
        public long Id { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "فرمت کد ملی صحیح نیست")] // مثال برای اعتبارسنجی دقیق‌تر
        public string NationalCode { get; set; }
        
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string MobileNumber { get; set; }

        [Display(Name = "جنسیت")]
        public bool Sex { get; set; } // true: Male, false: Female

        [Display(Name = "تاریخ تولد")]
        public string Birthday { get; set; }



    }
}
