using System.ComponentModel.DataAnnotations;

namespace WA_Pharmacy.DTOs.Doctor
{
    public class DoctorEditDto
    {
        public int Id { get; set; }

        [Display(Name = "کد پزشک")]
        [MaxLength(8, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string DoctorNumber { get; set; }

        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string NationalCode { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "تخصص")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string Specialization { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string MobileNumber { get; set; }

        [Display(Name = "جنسیت")]
        public bool Sex { get; set; } // true: Male, false: Female

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Birthday { get; set; }

        [Display(Name = "کاربر مرتبط")]
        public string UserId { get; set; }
    }
}
